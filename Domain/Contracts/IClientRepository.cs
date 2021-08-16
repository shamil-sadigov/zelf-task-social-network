using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.Contracts
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);
        Task<Client> GetAsync(ClientId clientId);
    }
}