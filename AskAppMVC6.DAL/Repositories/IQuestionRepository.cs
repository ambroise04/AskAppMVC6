using AskAppMVC6.DAL.Entities;
using System;
using System.Collections.Generic;

namespace AskAppMVC6.DAL.Repositories
{
    public interface IQuestionRepository
    {
        bool Delete(Question entity);
        Question Get(int id);
        ICollection<Question> GetAll();
        ICollection<Question> GetByPredicate(Func<Question, bool> predicate);
        Question Insert(Question entity);
        Question Update(Question entity);
        void SaveChanges();
    }
}