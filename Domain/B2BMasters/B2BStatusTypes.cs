using System;
using Domain.Common;

namespace Domain.B2BMasters
{
    public class B2BStatusTypes : Entity<long>
    {
        public B2BStatusTypes()
        {
            // ORM
        }
        
        public B2BStatusTypes(long id, string code, string description, DateTime createdDate)
        {
            Id = id;
            Code = code;
            Description = description;
            CreatedDate = createdDate;
        }
        
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}