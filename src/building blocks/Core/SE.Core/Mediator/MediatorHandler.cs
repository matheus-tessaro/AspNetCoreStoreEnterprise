using FluentValidation.Results;
using MediatR;
using SE.Core.Messages;
using System.Threading.Tasks;

namespace SE.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public MediatorHandler(IMediator mediator) => _mediator = mediator;

        public async Task Publish<T>(T evnt) where T : Event =>
            await _mediator.Publish(evnt);

        public async Task<ValidationResult> Send<T>(T command) where T : Command =>
            await _mediator.Send(command);
    }
}
