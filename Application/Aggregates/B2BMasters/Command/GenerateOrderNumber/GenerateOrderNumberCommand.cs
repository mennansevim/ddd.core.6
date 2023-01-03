using MediatR;

namespace Application.B2BMasters.Command.GenerateOrderNumber
{
    public record GenerateOrderNumberCommand : IRequest<long>
    { }
}