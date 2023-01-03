using System;
using Domain.B2BMasters;
using Infrastructure.Persistence;
using Infrastructure.StateMachine.Constants;
using Infrastructure.StateMachine.Responses;

namespace Infrastructure.StateMachine.B2BStatusHistories
{
    public abstract class StatusStateBase
    {
        private readonly B2BContext _context;

        private readonly B2BStatusHistory _b2BStatusHistory;

        protected StatusStateBase(B2BContext context, B2BStatusHistory b2BStatusHistory)
        {
            _b2BStatusHistory = b2BStatusHistory;
            _context = context;
        }

        protected abstract B2BStatusTypeNames MyStatus { get; }

        public virtual StateMachineMoveResult CanMoveTo(B2BStatusTypeNames nextStatus)
        {
            return StateMachineMoveResult.True;
        }

        public ExecuterResult? Enter()
        {
            var executerResult = OnEnter();

            if (executerResult.HasError) return executerResult;

            var receivingOrderStatus = new B2BStatusHistory()
            {
                B2BId = _b2BStatusHistory.B2BId,
                StatusId = (int)MyStatus
            };

            _context.B2BStatusHistories.Add(receivingOrderStatus);

            executerResult.NewStatus = receivingOrderStatus;
            
            return executerResult;
        }

        protected virtual ExecuterResult? OnEnter()
        {
            return new ExecuterResult();
        }
    }
}