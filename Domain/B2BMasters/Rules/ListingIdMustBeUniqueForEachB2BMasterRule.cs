using System.Linq;
using Domain.B2BMasters;
using Domain.Common;

namespace Domain.B2BMasters.Rules;

public class ListingIdMustBeUniqueForEachB2BMasterRule : IBusinessRule
{
    private readonly B2BMaster _master;
    private readonly B2BMasterItem _item;

    public ListingIdMustBeUniqueForEachB2BMasterRule(B2BMaster master, B2BMasterItem item)
    {
        _master = master;
        _item = item;
    }

    public bool IsBroken() => _master.Items.Any(x => x.Product.ListingId == _item.Product.ListingId);

    public string Message => "ListingId must be unique for each b2b-master";
}