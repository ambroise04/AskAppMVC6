using AskAppMVC6.DAL.Repositories;

namespace AskAppMVC6.BL.UseCases.General
{
    public partial class GeneralCases
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IResponseRepository responseRepository;

        public GeneralCases(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository ?? throw new System.ArgumentNullException(nameof(questionRepository));
        }

        public GeneralCases(IResponseRepository responseRepository)
        {
            this.responseRepository = responseRepository ?? throw new System.ArgumentNullException(nameof(responseRepository));
        }
    }
}