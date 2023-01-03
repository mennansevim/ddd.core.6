using System.Threading;
using System.Threading.Tasks;
using Application.Common.Data;
using MediatR;

namespace Application.B2BMasters.Command.GenerateOrderNumber
{
    public class GenerateOrderNumberCommandHandler : IRequestHandler<GenerateOrderNumberCommand, long>
    {
        private readonly IUnitOfWork _uow;

        public GenerateOrderNumberCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<long> Handle(GenerateOrderNumberCommand request, CancellationToken cancellationToken)
        {
            return await _uow.MasterRepository.GenerateOrderParentId();
        }
    }
}