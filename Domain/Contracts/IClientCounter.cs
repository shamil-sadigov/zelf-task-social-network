using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.Contracts
{
    public interface IClientCounter
    {
        Task<int> CountByNameAsync(ClientName name);
    }
}