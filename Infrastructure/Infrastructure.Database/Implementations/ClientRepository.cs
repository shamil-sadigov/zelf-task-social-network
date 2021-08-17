#region

using System.Threading.Tasks;
using Domain;
using Domain.Contracts;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Infrastructure.Database.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _context;

        public ClientRepository(ApplicationContext context) 
            => _context = context;

        public async Task AddAsync(Client client) 
            => await _context.AddAsync(client);

        public async Task<Client> GetAsync(ClientId clientId) 
            => await _context.Clients
                .Include("_subscribers")
                .FirstOrDefaultAsync(x=> x.Id == clientId);

        public void Update(Client client) 
            => _context.Entry(client).CurrentValues.SetValues(client);
    }
}