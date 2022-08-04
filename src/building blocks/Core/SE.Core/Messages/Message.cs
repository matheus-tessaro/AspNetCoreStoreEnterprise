using System;

namespace SE.Core.Messages
{
    public abstract class Message
    {
        protected Message() => MessageType = GetType().Name;

        public Guid AggregatedId { get; protected set; }
        public string MessageType { get; protected set; }
    }
}
