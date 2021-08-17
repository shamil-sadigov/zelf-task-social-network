#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Queries;

#endregion

namespace Application.Contracts
{
    public interface IClientQueryRepository
    {
        Task<List<ClientDto>?> GetTopPopularAsync(ushort limit);
        Task<List<ClientDto>?> GetClientSubscribersAsync(Guid clientId);
        Task<ClientDto?> GetByIdAsync(Guid clientId);
    }
}