using AskAppMVC6.BL.UseCases.General;
using AskAppMVC6.Common;
using AskAppMVC6.Common.Enumerations;
using AskAppMVC6.DAL.Entities;
using AskAppMVC6.DAL.Repositories;
using AskAppMVC6.UI.Extensions;
using AskAppMVC6.UI.Models;
using AskAppMVC6.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AskAppMVC6.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionRepository _responseRepository;
        private readonly IHubContext<QuestionHub> _hubContext;
        public GeneralCases generalCase;

        public HomeController(ILogger<HomeController> logger, 
            IQuestionRepository questionRepository, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHubContext<QuestionHub> hubContext)
        {
            _logger = logger;
            _userManager = userManager;
            _questionRepository = questionRepository;
            generalCase = new GeneralCases(_questionRepository);
            _signInManager = signInManager;
            _hubContext = hubContext;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetQuestions(string search)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var isAdmin = _userManager.IsInRoleAsync(currentUser, Roles.Admin.ToString()).Result;

            var questions = string.IsNullOrEmpty(search) ? generalCase.GetNotResolvedQuestions() : generalCase.GetSearchResult(search);
            var viewQuestions = new List<QuestionViewModel>();
            foreach (var qst in questions)
            {
                var responses = new List<ResponseViewModel>();
                foreach (var response in qst.Responses.OrderByDescending(r => r.IsTheBest).ThenByDescending(r => r.DateOfResponse))
                {
                    responses.Add(new ResponseViewModel
                    {
                        Id = response.Id,
                        QuestionId = qst.Id,
                        Message = response.Message,
                        ElapsedTime = response.DateOfResponse.ElapsedTime(),
                        Responder = string.Concat(response.Responder.FirstName, " ", response.Responder.LastName),
                        IsTheBest = response.IsTheBest,
                        CanBeMarkedAsTheBest = (!response.IsTheBest) && (qst.Requester.Id.Equals(currentUser.Id) || isAdmin) ? "" : "d-none"
                    });
                }
                viewQuestions.Add(new QuestionViewModel
                {
                    Id = qst.Id,
                    Message = qst.Message,
                    Publisher = string.Concat(qst.Requester.FirstName, " ", qst.Requester.LastName),
                    ElapsedTime = qst.PostDate.ElapsedTime(),
                    Responses = responses,
                    NumberOfResponses = responses.Count(),
                    DisableDeletion = (qst.Requester.Id.Equals(currentUser.Id) || isAdmin) ? "" : "d-none"
                });
            }
           
            return Json(new { questions = viewQuestions });
        }
        
        public async Task<IActionResult> Create(string message)
        {
            var user = await _userManager.GetUserAsync(User);
            var question = new Question
            {
                Message = message,
                IsDeleted = false,
                PostDate = DateTime.Now,
                State = QuestionState.Waiting,
                Requester = user
            };
            try
            {
                var result = generalCase.AddQuestion(question);
                _questionRepository.SaveChanges();
                if (result != null)
                {
                    await _hubContext.Clients.All.SendAsync("Refresh");
                    return Json(new { status = true, message = "Message posted successfully" });
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Json(new { status = false, message = "Sorry, an error was encountered. Please try again later." });
            }

            return Json(new { status = false, message = "Sorry, an error was encountered. Please try again later." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply(int target, string message)
        {
            if (target <= 0 || string.IsNullOrEmpty(message))
                return Json(new { status = false, message = "An error related to the arguments was encountered. Please try again." });

            var question = generalCase.GetQuestionById(target);
            if (question is null)
                return Json(new { status = false, message = "No question was found." });

            var response = new Response
            {
                Message = message,
                DateOfResponse = DateTime.Now,
                IsDeleted = false,
                Question = question,
                Responder = _userManager.GetUserAsync(User).Result
            };

            try
            {
                if (question.Responses is null)
                    question.Responses = new List<Response>();

                var result = generalCase.AddResponse(question, response);
                _questionRepository.SaveChanges();

                if (result is null)
                {
                    return Json(new { status = false, message = "Sorry, an error was encountered. Please try again later." });
                }

                await _hubContext.Clients.All.SendAsync("Refresh");
                _logger.LogInformation("Response added successfully");
                return Json(new { status = true, message = "Your response has been sent successfully." });
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Json(new { status = false, message = "Sorry, an error was encountered. Please try again later." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0 )
                return Json(new { status = false, message = "An error related to the arguments was encountered. Please try again." });

            var question = generalCase.GetQuestionById(id);
            if (question is null)
                return Json(new { status = false, message = "No question was found." });

            try
            {         
                var result = generalCase.DeleteQuestion(question);
                _questionRepository.SaveChanges();

                if (result)
                {
                    await _hubContext.Clients.All.SendAsync("Refresh");
                    return Json(new { status = true, message = "Question deleted successfully." });
                }                 
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return Json(new { status = false, message = "Sorry, an error was encountered. Please try again later." });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeBest(int responseId, int questionId)
        {
            var question = generalCase.GetQuestionById(questionId);

            var currentBest = question.Responses.FirstOrDefault(r => r.IsTheBest);
            if (currentBest != null)            
                currentBest.IsTheBest = false;     
            
            var newBest = question.Responses.FirstOrDefault(r => r.Id == responseId);
            if (newBest != null)
                newBest.IsTheBest = true;

            try
            {
                var result = generalCase.UpdateQuestion(question);
                _questionRepository.SaveChanges();

                if (result != null)
                {
                    await _hubContext.Clients.All.SendAsync("Refresh");
                    return Json(new { status = true, message = "Response marked successfully" });
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return Json(new { status = false, message = "Sorry, an error was encountered. Please try again later." });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void LogError(Exception exception)
        {
            _logger.LogError(exception, "An error occurs");
        }
    }
}