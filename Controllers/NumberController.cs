using Microsoft.AspNetCore.Mvc;

namespace Parcial1_P4_Chayanne.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class NumberController : ControllerBase
    {
        [HttpGet("{Number}")]
        public IActionResult Index([FromQuery] int Number)
        {
            return Ok(Number + Number);
        }
    }
}
