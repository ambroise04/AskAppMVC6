using AskAppMVC6.BL.UseCases.General;
using AskAppMVC6.Common.Enumerations;
using AskAppMVC6.DAL.Entities;

namespace AskAppMVC6.BL.UseCases.Special
{
    public partial class SpecialCases : GeneralCases
    {
        public bool MaskAsResolved(Question question)
        {
            question.State = QuestionState.Resolved;
            var result = questionRepository.Update(question);

            return result != null;
        }
    }
}