using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}