using FluentValidation.Results;
using SE.Core.Messages;
using System.Threading.Tasks;

namespace SE.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task Publish<T>(T evnt) where T : Event;
        Task<ValidationResult> Send<T>(T command) where T : Command;
    }
}
