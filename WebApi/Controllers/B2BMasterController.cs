using Application.B2BMasters.Command.GenerateOrderNumber;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("b2b-master")]
public class B2BMasterController : ControllerBase
{
    private readonly ISender _sender;

    public B2BMasterController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("order-parent-id")]
    public async Task<long> GenerateOrderNumber()
    {
        return await _sender.Send(new GenerateOrderNumberCommand());
    }
}
