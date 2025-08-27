using EFakturCallback;
using Microsoft.AspNetCore.Mvc;

namespace EFakturCallback.Controllers;

[ApiController]
[Route("e-faktur/listener")]
public class EFakturListenerController : SecureController
{
    readonly IEfakturListenerRepository repository;

    public EFakturListenerController(
        IEfakturListenerRepository repository
    )
    {
        this.repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> EFakturListener(FakturDetailStream message)
    {
        return Response();
    }
}