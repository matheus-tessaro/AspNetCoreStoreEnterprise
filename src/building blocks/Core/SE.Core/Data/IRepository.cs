using SE.Core.DomainObjects;
using System;

namespace SE.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
    }
}
