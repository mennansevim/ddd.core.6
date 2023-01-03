using System;
using System.ComponentModel;
using Domain.Common;

namespace Domain.B2BMasters
{
    public class B2BStatusHistory : Entity<long>
    {
        public B2BStatusHistory()
        {
            // ORM
        }

        public B2BStatusHistory(long id, long b2BId, int statusId)
        {
            Id = id;
            B2BId = b2BId;
            StatusId = statusId;
            CreationDate = DateTime.Now;
        }
        
        public long Id { get; set; }
        public long B2BId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}