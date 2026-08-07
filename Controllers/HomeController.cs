using Microsoft.AspNetCore.Mvc;
namespace curd_api.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                name = "Task API",
                version = "1.0",
                endpoints = new[]
                {
                    "/tasks"
                }
            });
        }
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "ok"
            });
        }
    }
}
