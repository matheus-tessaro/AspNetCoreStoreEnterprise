using FluentValidation.Results;
using MediatR;
using System;

namespace SE.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        protected Command() => TimeStamp = DateTime.Now;

        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool Invalid()
        {
            throw new NotImplementedException();
        }
    }
}
