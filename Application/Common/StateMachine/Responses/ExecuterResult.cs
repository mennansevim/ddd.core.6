using System.Collections.Generic;
using System.ComponentModel;
using Domain.B2BMasters;

namespace Infrastructure.StateMachine.Responses
{
    public class ExecuterResult
    {
        public ExecuterResult()
        {
            Events = new List<object>();
        }

        public List<object> Events { get; set; }

        [DefaultValue(false)] 
        public bool HasError { get; set; }
        public string Message { get; set; }
        public B2BStatusHistory? NewStatus { get; set; }
    }
}