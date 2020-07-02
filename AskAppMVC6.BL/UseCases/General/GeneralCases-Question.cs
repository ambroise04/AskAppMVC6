using AskAppMVC6.Common.Enumerations;
using AskAppMVC6.DAL.Entities;
using System.Collections.Generic;

namespace AskAppMVC6.BL.UseCases.General
{
    public partial class GeneralCases
    {
        public Question AddQuestion(Question question)
        {
            return questionRepository.Insert(question);
        }
        public Question UpdateQuestion(Question question)
        {
            return questionRepository.Update(question);
        }
        public bool DeleteQuestion(Question question)
        {
            return questionRepository.Delete(question);
        }

        public Question MarkAs(QuestionState state, Question question)
        {
            question.State = state;
            return questionRepository.Update(question);
        }

        public Question GetQuestionById(int id)
        {
            return questionRepository.Get(id);
        }

        public ICollection<Question> GetNotResolvedQuestions()
        {
            return questionRepository.GetByPredicate(q => q.State != QuestionState.Resolved);
        }

        public ICollection<Question> GetResolvedQuestions()
        {
            return questionRepository.GetByPredicate(q => q.State == QuestionState.Resolved);
        }

        public ICollection<Question> GetSearchResult(string key)
        {
            return questionRepository.GetByPredicate(q => q.State != QuestionState.Resolved
                            && (q.Message.ToLower().Contains(key.Trim().ToLower()) 
                                || string.Concat(q.Requester.FirstName, " ", q.Requester.LastName).ToLower().Contains(key.Trim().ToLower())));
        }
    }
}