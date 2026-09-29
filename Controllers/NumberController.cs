using Microsoft.AspNetCore.Mvc;

namespace Parcial1_P4_Chayanne.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class NumberController : Controller
    {
        [HttpGet]
        public IActionResult Index([FromQuery]int Number)
        {
            return Ok(Number + Number);
        }
    }
}
