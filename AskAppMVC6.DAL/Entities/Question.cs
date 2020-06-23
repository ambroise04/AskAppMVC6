using AskAppMVC6.Common.Enumerations;
using System;
using System.Collections.Generic;

namespace AskAppMVC6.DAL.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public ApplicationUser Requester { get; set; }
        public DateTime PostDate { get; set; }
        public ICollection<Response> Responses { get; set; }
        public QuestionState State { get; set; }
        public bool IsDeleted { get; set; }
    }
}