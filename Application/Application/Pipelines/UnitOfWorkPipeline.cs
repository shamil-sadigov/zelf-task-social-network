#region

using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using MediatR;

#endregion

namespace Application.Pipelines
{
    public class UnitOfWorkPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IDomainEventsPublisher _domainEventsPublisher;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkPipeline(IDomainEventsPublisher domainEventsPublisher, IUnitOfWork unitOfWork)
        {
            _domainEventsPublisher = domainEventsPublisher;
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> nextHandler)
        {
            var response = await nextHandler();

            await _domainEventsPublisher.PublishEventsAsync();

            await _unitOfWork.CommitAsync();

            return response;
        }
    }
}