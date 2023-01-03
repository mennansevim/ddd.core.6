using Domain.Common;

namespace Domain.B2BMasters.Rules
{
    public class ItemCountCanNotBeGreaterThanThreshold : IBusinessRule
    {
        private readonly B2BMaster _master;
        
        public ItemCountCanNotBeGreaterThanThreshold(B2BMaster master)
        {
            _master = master;
        }

        public bool IsBroken() => false;

        public string Message => "B2BMasterItemCount can not exceed to max item threshold";
    }
}