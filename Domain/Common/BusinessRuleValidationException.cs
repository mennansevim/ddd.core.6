using System;

namespace Domain.Common
{
    public class BusinessRuleValidationException : Exception
    {
        private readonly IBusinessRule _businessRule;

        public BusinessRuleValidationException(IBusinessRule brokenRule) : base(brokenRule.Message)
        {
            _businessRule = brokenRule;
        }

        public override string ToString()
        {
            return $"{_businessRule.GetType().Name}: {_businessRule.Message}";
        }
    }
}