#region

using System.Threading.Tasks;
using Domain.ValueObjects;

#endregion

namespace Domain.Contracts
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);
        Task<Client> GetAsync(ClientId clientId);
        void Update(Client client);
    }
}