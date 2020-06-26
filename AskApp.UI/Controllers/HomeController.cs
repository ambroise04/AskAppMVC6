using AskAppMVC6.BL.UseCases.General;
using AskAppMVC6.Common.Enumerations;
using AskAppMVC6.DAL.Entities;
using AskAppMVC6.DAL.Repositories;
using AskAppMVC6.UI.Extensions;
using AskAppMVC6.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AskAppMVC6.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IQuestionRepository _questionRepository;
        public GeneralCases generalCase ;

        public HomeController(ILogger<HomeController> logger, IQuestionRepository questionRepository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            _questionRepository = questionRepository;
            generalCase = new GeneralCases(_questionRepository);
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetQuestions()
        {
            var questions = generalCase.GetNotResolvedQuestions();
            var viewQuestions = new List<QuestionViewModel>();
            foreach (var qst in questions)
            {
                var responses = new List<ResponseViewModel>();
                foreach (var response in qst.Responses)
                {
                    responses.Add(new ResponseViewModel
                    {
                        Id = response.Id,
                        Message = response.Message,
                        ElapsedTime = response.DateOfResponse.ElapsedTime(),
                        Responder = string.Concat(response.Responder.FirstName, " ", response.Responder.LastName),
                    });
                }
                viewQuestions.Add(new QuestionViewModel
                {
                    Id = qst.Id,
                    Message = qst.Message,
                    Publisher = string.Concat(qst.Requester.FirstName, " ", qst.Requester.LastName),
                    ElapsedTime = qst.PostDate.ElapsedTime(),
                    Responses = responses
                });
            }
           
            return Json(new { questions = viewQuestions });
        }

        [Authorize]
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
                    return Json(new { status = true, message = "Message posted" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Message posted" });
            }

            return Json(new { status = false, message = "Message posted"});
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
    }
}