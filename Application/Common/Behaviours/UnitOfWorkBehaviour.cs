using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Data;

namespace Application.Common.Behaviours
{
    public class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _uow;

        public UnitOfWorkBehaviour(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var response = await next();
            await _uow.CommitAsync(cancellationToken);
            return response;
        }
    }
}