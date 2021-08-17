using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IDomainEventsPublisher
    {
        Task PublishEventsAsync();
    }
}