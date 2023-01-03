using Domain.Common;

namespace Domain.B2BMasters.Rules
{
    public class MasterStatusCanNotBeShippedIfNotApproved : IBusinessRule
    {
        private readonly B2BMaster _master;
        
        public MasterStatusCanNotBeShippedIfNotApproved(B2BMaster master)
        {
            _master = master;
        }

        public bool IsBroken() => _master.Status != B2BMasterStatus.Approved;

        public string Message => "B2BMaster must be approved for shipping";
    }
}