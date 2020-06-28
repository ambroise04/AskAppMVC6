using System.Collections.Generic;

namespace AskAppMVC6.UI.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Publisher { get; set; }
        public string Message { get; set; }
        public string ElapsedTime { get; set; }
        public ICollection<ResponseViewModel> Responses { get; set; }
        public int NumberOfResponses { get; set; }
        public string DisableDeletion { get; set; }
    }
}