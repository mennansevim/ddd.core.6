using System;

namespace Application.Common
{
    public interface IExecutionContextAccessor
    {
        Guid CorrelationId { get; }
    }
}