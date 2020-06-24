using AskAppMVC6.DAL.Repositories;

namespace AskAppMVC6.BL.UseCases.General
{
    public partial class GeneralCases
    {
        private readonly QuestionRepository questionRepository;
        private readonly ResponseRepository responseRepository;

        public GeneralCases(QuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository ?? throw new System.ArgumentNullException(nameof(questionRepository));
        }

        public GeneralCases(ResponseRepository responseRepository)
        {
            this.responseRepository = responseRepository ?? throw new System.ArgumentNullException(nameof(responseRepository));
        }
    }
}