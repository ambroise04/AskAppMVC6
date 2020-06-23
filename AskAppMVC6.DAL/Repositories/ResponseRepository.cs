using AskAppMVC6.DAL.Context;
using AskAppMVC6.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AskAppMVC6.DAL.Repositories
{
    public class ResponseRepository : Persistance, IRepository<Response>
    {
        private readonly ApplicationContext context;

        public ResponseRepository(ApplicationContext context) : base(context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Delete(Response entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id <= 0)
                throw new ArgumentException();

            entity.IsDeleted = true;
            var result = Update(entity);

            return result != null;
        }

        public Response Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException();

            var response = context.Responses
                                   .Include(r => r.Question)                                  
                                   .Include(r => r.Responder)
                                   .FirstOrDefault(r => r.Id == id);


            return response;
        }

        public ICollection<Response> GetAll()
        {
            var responses = context.Responses
                                   .Include(r => r.Question)
                                   .Include(r => r.Responder)
                                   .Where(q => !q.IsDeleted);

            return responses.ToList();
        }

        public ICollection<Response> GetByPredicate(Func<Response, bool> predicate)
        {
            var responses = context.Responses
                                   .Include(r => r.Question)
                                   .Include(r => r.Responder)
                                   .Where(q => !q.IsDeleted)
                                   .Where(predicate);

            return responses.ToList();
        }

        public Response Insert(Response entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var entityEntry = context.Responses.Add(entity);

            return entityEntry.Entity;
        }

        public Response Update(Response entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (entity.Id <= 0)
                throw new ArgumentException();

            context.Responses.Attach(entity).State = EntityState.Modified;

            return entity;
        }
    }
}