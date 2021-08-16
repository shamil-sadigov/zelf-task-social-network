using Domain.ValueObjects;

namespace Domain.Contracts
{
    public interface IClientCounter
    {
        int CountByName(ClientName name);
    }
}