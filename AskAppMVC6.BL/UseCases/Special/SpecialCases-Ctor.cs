using AskAppMVC6.BL.UseCases.General;
using AskAppMVC6.DAL.Repositories;
using System;

namespace AskAppMVC6.BL.UseCases.Special
{
    public partial class SpecialCases : GeneralCases
    {
        private readonly QuestionRepository questionRepository;
        private readonly ResponseRepository responseRepository;

        public SpecialCases(QuestionRepository questionRepository) : base(questionRepository)
        {           
            this.questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        }

        public SpecialCases(ResponseRepository responseRepository) : base(responseRepository)
        {
            this.responseRepository = responseRepository ?? throw new ArgumentNullException(nameof(responseRepository));
        }
    }
}