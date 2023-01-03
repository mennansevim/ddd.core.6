using System;
using Infrastructure.Common.Data;

namespace Infrastructure.StateMachine.Responses
{
    public class GetB2BStatusHistoryResponse : BaseResponse
    {
        public long Id { get; set; }
        public long B2bId     { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}