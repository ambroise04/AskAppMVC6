using System;

namespace AskAppMVC6.DAL.Entities
{
    public class Response
    {
        public int Id { get; set; }
        public Question Question { get; set; }
        public ApplicationUser Responder { get; set; }
        public DateTime DateOfResponse { get; set; }
        public bool IsDeleted { get; set; }
    }
}