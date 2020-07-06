namespace AskAppMVC6.UI.Models
{
    public class ResponseViewModel
    {
        public int Id { get; set; }
        public string Responder { get; set; }
        public string Message { get; set; }
        public string ElapsedTime { get; set; }
        public bool IsTheBest { get; set; }
    }
}