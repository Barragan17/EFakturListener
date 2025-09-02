using EFakturCallback;
using Microsoft.AspNetCore.Mvc;
using EFakturCallback.Data.Repositories.Interfaces;
using EFakturCallback.Data.Entities;
using System.Text.Json;
using Serilog;

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
    public async Task<IActionResult> EFakturListener([FromBody] FakturDetailStream message)
    {
        try
        {
            Log.Information($"Payload: {JsonSerializer.Serialize(message)}");
            await repository.EFakturListener(message);
            return Response();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            AddError(ex.Message, ex.Message, ex.Message);
            return Response();
        }
    }
}