using Domain.B2BMasters;
using Infrastructure.Persistence;
using Infrastructure.StateMachine.Responses;

namespace Infrastructure.StateMachine.B2BStatusHistories
{
    public static class StateExtensions
    {
        public static ExecuterResult? MoveTo(this B2BStatusHistory b2BStatusHistory, B2BContext context,
            B2BStatusTypeNames nextStatus)
        {
            var currentStatus = (B2BStatusTypeNames) b2BStatusHistory.StatusId;
            
            var statusStateMachine = StatusStateMachine.Restore(currentStatus, context, b2BStatusHistory);
            var executeResult = statusStateMachine.MoveTo(nextStatus);
            return executeResult; 
        }
    }
}