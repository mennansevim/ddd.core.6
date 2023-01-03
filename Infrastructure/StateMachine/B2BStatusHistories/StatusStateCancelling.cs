using Domain.B2BMasters;
using Infrastructure.Persistence;
using Infrastructure.StateMachine.Constants;

namespace Infrastructure.StateMachine.B2BStatusHistories
{
    public class StatusStateCancelling : StatusStateBase
    {
        public StatusStateCancelling(B2BContext context, B2BStatusHistory b2BStatusHistory) :
            base(context, b2BStatusHistory)
        {
        }

        protected override B2BStatusTypeNames MyStatus => B2BStatusTypeNames.Canceling;

        public override StateMachineMoveResult CanMoveTo(B2BStatusTypeNames nextStatus)
        {
            return nextStatus == B2BStatusTypeNames.Cancelled || nextStatus == MyStatus
                ? StateMachineMoveResult.True : 
                StateMachineMoveResult.False(string.Format(StateMachineMessages.StatusMustBeMoveLinearErrorF, MyStatus, nextStatus));
        }
    }
}