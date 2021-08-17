using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Queries;

namespace Application.Contracts
{
    public interface IClientQueryRepository
    {
        Task<List<ClientDto>> GetTopPopularAsync(ushort limit);
        Task<ClientDto?> GetByIdAsync(Guid clientId);
    }
}