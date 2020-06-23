using AskAppMVC6.DAL.Context;
using AskAppMVC6.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AskAppMVC6.DAL.Repositories
{
    public class QuestionRepository : Persistance, IRepository<Question>
    {
        private readonly ApplicationContext context;

        public QuestionRepository(ApplicationContext context) : base(context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Delete(Question entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id <= 0)
                throw new ArgumentException();

            entity.IsDeleted = true;
            var result = Update(entity);

            return result != null;
        }

        public Question Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException();

            var question = context.Questions
                                  .Include(q => q.Requester)
                                  .Include(q => q.Responses)
                                  .ThenInclude(r => r.Responder)
                                  .FirstOrDefault(q => q.Id == id);
                
            return question;
        }

        public ICollection<Question> GetAll()
        {
            var questions = context.Questions
                                   .Include(q => q.Requester)
                                   .Include(q => q.Responses)
                                   .ThenInclude(r => r.Responder)
                                   .Where(q => !q.IsDeleted);

            return questions.ToList();
        }

        public ICollection<Question> GetByPredicate(Func<Question, bool> predicate)
        {
            var questions = context.Questions
                                   .Include(q => q.Requester)
                                   .Include(q => q.Responses)
                                   .ThenInclude(r => r.Responder)
                                   .Where(q => !q.IsDeleted)
                                   .Where(predicate);

            return questions.ToList();
        }

        public Question Insert(Question entity)
        {
            if (entity is null)            
                throw new ArgumentNullException(nameof(entity));

            var entityEntry = context.Questions.Add(entity);

            return entityEntry.Entity;
        }

        public Question Update(Question entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id <= 0)
                throw new ArgumentException();

            context.Questions.Attach(entity).State = EntityState.Modified;

            return entity;
        }
    }
}