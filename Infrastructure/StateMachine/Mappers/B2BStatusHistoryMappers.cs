using System;
using System.Collections.Generic;
using Domain.B2BMasters;
using Infrastructure.StateMachine.Responses;

namespace Infrastructure.StateMachine.Mappers
{
    public static class B2BStatusHistoryMappers
    {
        public static GetB2BStatusHistoryResponse ToModel(this B2BStatusHistory? b2BStatusHistory)
        {
            if (b2BStatusHistory == null) throw new ArgumentNullException(nameof(b2BStatusHistory));

            return new GetB2BStatusHistoryResponse
            {
                Messages = new List<MessageDto>(),
                Id = b2BStatusHistory.Id,
                B2bId = b2BStatusHistory.B2BId,
                StatusId = b2BStatusHistory.StatusId,
                CreationDate = b2BStatusHistory.CreationDate
            };
        }

        public static List<GetB2BStatusHistoryResponse> ToModel(this List<B2BStatusHistory> offerStatusList)
        {
            if (offerStatusList == null) throw new ArgumentNullException(nameof(offerStatusList));

            var offerStatusResponseList = new List<GetB2BStatusHistoryResponse>();
            offerStatusList.ForEach(p => offerStatusResponseList.Add(p.ToModel()));
            return offerStatusResponseList;
        }
    }
}