using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.B2BMasters;
using Infrastructure.Persistence;
using Infrastructure.StateMachine.B2BStatusHistories;
using Infrastructure.StateMachine.Constants;
using Infrastructure.StateMachine.Mappers;
using Infrastructure.StateMachine.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.StateMachine.Services
{
    public class B2BStatusHistoryService : IB2BStatusHistoryService
    {
        private readonly B2BContext _b2BDataContext;
        private GetB2BStatusHistoryResponse _response;

        public B2BStatusHistoryService(B2BContext b2BDataContext)
        {
            _b2BDataContext = b2BDataContext;
            _response = new GetB2BStatusHistoryResponse();
        }

        public async Task<GetB2BStatusHistoryResponse> AppendNew(long b2BId, int statusId)
        {
            if (!await IsB2BIdExist(b2BId)) return _response;

            var b2BStatusHistory = await GetB2B2BStatusHistory(b2BId);

            var newStatus = new B2BStatusHistory
            {
                B2BId = b2BId,
                StatusId = statusId
            };

            if (b2BStatusHistory == null)
            {
                if (statusId != (int)B2BStatusTypeNames.PickingWaiting)
                {
                    _response.Messages.Add(new MessageDto(StateMachineMessages.FirstStatusShouldBePickingWaitingMessage, MessageType.Error));
                    return _response;
                }

                _b2BDataContext.B2BStatusHistories.Add(newStatus);
            }
            else
            {
                var result = b2BStatusHistory.MoveTo(_b2BDataContext, (B2BStatusTypeNames)statusId);
                if (result != null && result.HasError)
                {
                    _response.Messages.Add(new MessageDto(result.Message, MessageType.Error));
                    return _response;
                }
            }

            await _b2BDataContext.SaveChangesAsync();
            _response = newStatus.ToModel();

            return _response;
        }
        
        public async Task AppendNewCommand(long b2BId, int statusId)
        {
            if (!await IsB2BIdExist(b2BId))
                throw new NotFoundException(nameof(B2BMaster), b2BId);

            var b2BStatusHistory = await GetB2B2BStatusHistory(b2BId);

            var newStatus = new B2BStatusHistory
            {
                B2BId = b2BId,
                StatusId = statusId
            };

            if (b2BStatusHistory == null)
            {
                if (statusId != (int)B2BStatusTypeNames.PickingWaiting)
                {
                    throw new BusinessException(StateMachineMessages.FirstStatusShouldBePickingWaitingMessage);
                }

                _b2BDataContext.B2BStatusHistories.Add(newStatus);
            }
            else
            {
                var result = b2BStatusHistory.MoveTo(_b2BDataContext, (B2BStatusTypeNames)statusId);
                if (result is { HasError: true })
                {
                    throw new BusinessException(result.Message);
                }
            }

            if (b2BStatusHistory.StatusId == statusId)
            {
                _b2BDataContext.ChangeTracker.Clear();
                return;
            }

            await _b2BDataContext.SaveChangesAsync();
        }

        private async Task<B2BStatusHistory?> GetB2B2BStatusHistory(long b2BId)
        {
            return await _b2BDataContext.B2BStatusHistories.OrderByDescending(x => x.StatusId)
                .FirstOrDefaultAsync(x => x != null && x.B2BId == b2BId);
        }

        private async Task<bool> IsB2BIdExist(long b2BId)
        {
            if (await _b2BDataContext.B2BMasters.AnyAsync(x => x.Id == b2BId)) 
                return true;
            
            _response.Messages.Add(new MessageDto("B2BId bulunamadı !", MessageType.Error));
            
            return false;

        }
    }
}