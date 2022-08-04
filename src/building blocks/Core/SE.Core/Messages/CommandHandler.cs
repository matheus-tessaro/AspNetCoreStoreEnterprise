using FluentValidation.Results;
using SE.Core.Data;
using System.Threading.Tasks;

namespace SE.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler() => ValidationResult = new ValidationResult();

        protected void AddError(string message) => ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));

        protected async Task<ValidationResult> Commit(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Commit())
                AddError("An error occured while trying to commit data");

            return ValidationResult;
        }
    }
}
