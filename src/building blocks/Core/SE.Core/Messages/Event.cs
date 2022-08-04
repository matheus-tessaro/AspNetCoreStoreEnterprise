using MediatR;
using System;

namespace SE.Core.Messages
{
    public class Event : Message, INotification
    {
        protected Event() => TimeStamp = DateTime.Now;

        public DateTime TimeStamp { get; private set; }
    }
}
