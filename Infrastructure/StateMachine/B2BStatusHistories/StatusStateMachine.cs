using System;
using System.Collections.Generic;
using Domain.B2BMasters;
using Infrastructure.Persistence;
using Infrastructure.StateMachine.Constants;
using Infrastructure.StateMachine.Responses;

namespace Infrastructure.StateMachine.B2BStatusHistories
{
    public sealed class StatusStateMachine
    {
        private static Dictionary<B2BStatusTypeNames, RuntimeTypeHandle>? _stateMapping;
        private readonly B2BContext _dataContext;
        private readonly B2BStatusHistory _b2BStatusHistory;
        private StatusStateBase _myCurrentState;

        private StatusStateMachine(StatusStateBase currentState, B2BContext dataContext,
            B2BStatusHistory b2BStatusHistory)
        {
            _myCurrentState = currentState;
            _b2BStatusHistory = b2BStatusHistory;
            _dataContext = dataContext;
        }

        private static Dictionary<B2BStatusTypeNames, RuntimeTypeHandle>? StateMapping
        {
            get
            {
                if (_stateMapping != null) return _stateMapping;
                _stateMapping = new Dictionary<B2BStatusTypeNames, RuntimeTypeHandle>
                {
                    { B2BStatusTypeNames.PickingWaiting, typeof(StatusStatePickingWaiting).TypeHandle },
                    { B2BStatusTypeNames.PickingStarted, typeof(StatusStatePickingStarted).TypeHandle },
                    { B2BStatusTypeNames.WeighingCompleted, typeof(StatusStateWeighingCompleted).TypeHandle },
                    { B2BStatusTypeNames.Canceling, typeof(StatusStateCancelling).TypeHandle },
                    { B2BStatusTypeNames.Cancelled, typeof(StatusStateCancelled).TypeHandle },
                    { B2BStatusTypeNames.WaitingShipment, typeof(StatusStateWaitingShipment).TypeHandle },
                    { B2BStatusTypeNames.Invoicing, typeof(StatusStateInvoicing).TypeHandle },
                    { B2BStatusTypeNames.InvoiceReady, typeof(StatusStateInvoiceReady).TypeHandle },
                    { B2BStatusTypeNames.InvoiceCreated, typeof(StatusStateInvoiceCreated).TypeHandle },
                    { B2BStatusTypeNames.Shipped, typeof(StatusStateShipped).TypeHandle }
                };

                return _stateMapping;
            }
        }

        public static StatusStateMachine Restore(B2BStatusTypeNames currentState, B2BContext dataContext,
            B2BStatusHistory b2BStatusHistory)
        {
            var stateType = Type.GetTypeFromHandle(StateMapping[currentState]);
            var ctorInfo = stateType.GetConstructor(new[] {typeof(B2BContext), typeof(B2BStatusHistory)});
            var instance = (StatusStateBase) ctorInfo?.Invoke(new object[] {dataContext, b2BStatusHistory})!;

            return new StatusStateMachine(instance, dataContext, b2BStatusHistory);
        }

        public StateMachineMoveResult CanMoveTo(B2BStatusTypeNames nextState)
        {
            return _myCurrentState.CanMoveTo(nextState);
        }

        public ExecuterResult? MoveTo(B2BStatusTypeNames nextStatus)
        {
            if (_stateMapping != null && !_stateMapping.ContainsKey(nextStatus))
            {
                return new ExecuterResult
                {
                    HasError = true,
                    Message = StateMachineMessages.UnKnownStatus
                };
            }
            
            var currentStateMovement = _myCurrentState.CanMoveTo(nextStatus);

            if (!currentStateMovement.CanMove)
                return new ExecuterResult
                {
                    HasError = true,
                    Message = currentStateMovement.Message
                };
            
            var nextStateType = Type.GetTypeFromHandle(_stateMapping[nextStatus]);
            var ctorInfo = nextStateType.GetConstructor(new[] {typeof(B2BContext), typeof(B2BStatusHistory)});
            var nextStateInstance = (StatusStateBase) ctorInfo?.Invoke(new object[] {_dataContext, _b2BStatusHistory})!;

            _myCurrentState = nextStateInstance;
            return nextStateInstance?.Enter();
        }
    }
}