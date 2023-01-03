using System.Threading.Tasks;
using Infrastructure.StateMachine.Responses;

namespace Infrastructure.StateMachine.Services
{
    public interface IB2BStatusHistoryService
    {
        Task<GetB2BStatusHistoryResponse> AppendNew(long b2BId, int statusId);
        Task AppendNewCommand(long b2BId, int statusId);
    }
}