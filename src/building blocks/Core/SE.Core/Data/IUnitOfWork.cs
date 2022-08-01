using System.Threading.Tasks;

namespace SE.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
