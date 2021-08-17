#region

using System.Threading.Tasks;

#endregion

namespace Application.Contracts
{
    public interface IDomainEventsPublisher
    {
        Task PublishEventsAsync();
    }
}