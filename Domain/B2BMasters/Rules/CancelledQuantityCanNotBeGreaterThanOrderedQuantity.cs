using Domain.Common;

namespace Domain.B2BMasters.Rules
{
    public class CancelledQuantityCanNotBeGreaterThanOrderedQuantity : IBusinessRule
    {
        private readonly B2BMasterItem _masterItem;
        private readonly int _cancelledQuantity;
        
        public CancelledQuantityCanNotBeGreaterThanOrderedQuantity(B2BMasterItem masterItem, int cancelledQuantity)
        {
            _masterItem = masterItem;
            _cancelledQuantity = cancelledQuantity;
        }

        public bool IsBroken() => _cancelledQuantity > _masterItem.OrderedQuantity;

        public string Message => "B2BMasterItem cancelled quantity can not be greater than OrderedQuantity";
    }
}