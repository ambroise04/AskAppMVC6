using AskAppMVC6.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AskAppMVC6.DAL.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(){}

        public ApplicationContext(DbContextOptions options) : base(options)
        {           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var connectionString = Environment.GetEnvironmentVariable("SqliteConnection");
                optionsBuilder.UseSqlite("Data Source=AskApp.db");
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }
    }
}