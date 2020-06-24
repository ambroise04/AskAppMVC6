using AskAppMVC6.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AskAppMVC6.DAL.Entities
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public ApplicationUser Requester { get; set; }
        [Required]
        public DateTime PostDate { get; set; }
        public ICollection<Response> Responses { get; set; }
        [Required]
        public QuestionState State { get; set; }
        public bool IsDeleted { get; set; }
    }
}