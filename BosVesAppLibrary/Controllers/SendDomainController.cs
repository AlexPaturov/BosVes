using Microsoft.AspNetCore.Mvc;

namespace BosVesAppLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SendDomainController : ControllerBase
{
   [HttpPost]
   public IActionResult ReceiveDomain([FromBody] DomainRequest domainRequest)
   {
      // You can do further processing here with the domain name
      Console.WriteLine($"Received domain name: {domainRequest.DomainName}");
      return Ok();
   }
}

public class DomainRequest
{
   public string DomainName { get; set; }
}
