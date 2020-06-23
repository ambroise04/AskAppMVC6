using AskAppMVC6.DAL.Context;
using System;

namespace AskAppMVC6.DAL.Repositories
{
    public class Persistance : IDisposable
    {
        private readonly ApplicationContext context;

        public Persistance(ApplicationContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
            Dispose();
        }
        
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}