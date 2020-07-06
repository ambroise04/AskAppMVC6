using AskAppMVC6.DAL.Entities;
namespace AskAppMVC6.BL.UseCases.General
{
    public partial class GeneralCases
    {
        public Response AddResponse(Question question, Response response)
        {
            question.Responses.Add(response);
            UpdateQuestion(question);

            return response;
        }

        public bool MarkAsYheBest(Response response)
        {
            response.IsTheBest = true;

            var result = responseRepository.Update(response);

            return result != null;
        }
    }
}