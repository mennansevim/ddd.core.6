using Domain.Common;

namespace Domain.B2BMasters.Rules
{
    public class UnsuppliedQuantityCanNotBeGreaterThanOrderedQuantity : IBusinessRule
    {
        private readonly B2BMasterItem _masterItem;
        private readonly int _unsuppliedQuantity;
        
        public UnsuppliedQuantityCanNotBeGreaterThanOrderedQuantity(B2BMasterItem masterItem, int unsuppliedQuantity)
        {
            _masterItem = masterItem;
            _unsuppliedQuantity = unsuppliedQuantity;
        }

        public bool IsBroken() => _unsuppliedQuantity > _masterItem.OrderedQuantity;

        public string Message => "B2BMasterItem unsupplied quantity can not be greater than OrderedQuantity";
    }
}