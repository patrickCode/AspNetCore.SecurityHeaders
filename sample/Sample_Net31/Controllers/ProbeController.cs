using Microsoft.AspNetCore.Mvc;

namespace Sample_Net31.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProbeController: ControllerBase
    {
        [HttpGet]
        public IActionResult Ping([FromQuery] string echo = null)
        {
            return new OkObjectResult(echo ?? "Pong");
        }
    }
}
