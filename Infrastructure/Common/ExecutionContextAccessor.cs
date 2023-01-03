using System;
using Application.Common;

namespace Infrastructure.Common
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        public ExecutionContextAccessor()
        {
            CorrelationId = Guid.NewGuid();
        }

        public Guid CorrelationId { get; }
    }
}