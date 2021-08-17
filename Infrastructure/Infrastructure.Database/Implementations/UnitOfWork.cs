#region

using System.Threading.Tasks;
using Application.Contracts;

#endregion

namespace Infrastructure.Database.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
            => await _context.SaveChangesAsync();
    }
}