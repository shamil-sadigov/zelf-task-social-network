#region

using System.Threading.Tasks;
using Domain.ValueObjects;

#endregion

namespace Domain.Contracts
{
    public interface IClientCounter
    {
        Task<int> CountByNameAsync(ClientName name);
    }
}