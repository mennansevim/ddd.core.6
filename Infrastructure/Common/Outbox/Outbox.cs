using System.Threading.Tasks;
using Application.Common.Outbox;
using Infrastructure.Persistence;

namespace Infrastructure.Common.Outbox;

public class Outbox : IOutbox
{
    private readonly B2BContext _context;

    public Outbox(B2BContext context)
    {
        _context = context;
    }

    public async Task Add(OutboxMessage outbox)
    {
        await _context.OutboxMessages.AddAsync(outbox);
    }
}