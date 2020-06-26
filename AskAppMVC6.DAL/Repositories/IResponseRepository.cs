using AskAppMVC6.DAL.Entities;
using System;
using System.Collections.Generic;

namespace AskAppMVC6.DAL.Repositories
{
    public interface IResponseRepository
    {
        bool Delete(Response entity);
        Response Get(int id);
        ICollection<Response> GetAll();
        ICollection<Response> GetByPredicate(Func<Response, bool> predicate);
        Response Insert(Response entity);
        Response Update(Response entity);
        void SaveChanges();
    }
}