using System;
using System.ComponentModel.DataAnnotations;

namespace AskAppMVC6.DAL.Entities
{
    public class Response
    {
        public int Id { get; set; }
        [Required]
        public Question Question { get; set; }
        public string Message { get; set; }
        public ApplicationUser Responder { get; set; }
        [Required]
        public DateTime DateOfResponse { get; set; }
        public bool IsDeleted { get; set; }
    }
}