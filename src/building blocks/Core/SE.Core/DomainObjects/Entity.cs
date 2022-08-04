using SE.Core.Messages;
using System;
using System.Collections.Generic;

namespace SE.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity() => Id = Guid.NewGuid();

        public Guid Id { get; set; }

        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

        public void AddNotification(Event evnt)
        {
            _notifications ??= new List<Event>();
            _notifications.Add(evnt);
        }

        public void RemoveNotification(Event evnt) => _notifications?.Remove(evnt);

        public void ClearNotifications() => _notifications?.Clear();

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public override int GetHashCode() => (GetType().GetHashCode() * new Random().Next(2, 1000)) + Id.GetHashCode();

        public override string ToString() => $"{GetType().Name} [Id={Id}]";
    }
}
