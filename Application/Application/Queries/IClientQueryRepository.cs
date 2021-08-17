using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Queries
{
    public interface IClientQueryRepository
    {
        Task<List<ClientDto>> GetTopPopularAsync(ushort limit);
    }
}