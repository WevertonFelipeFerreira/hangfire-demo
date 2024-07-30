using Microsoft.AspNetCore.Mvc;

namespace HangfireDemo.Controllers
{
    [ApiController]
    [Route("jobs")]
    public class JobsController : ControllerBase
    {
        [HttpGet("fire-forget")]
        public IActionResult FireForget() 
        {
            return Ok();
        }

        [HttpGet("fire-forget-continuation")]
        public IActionResult FireForgetContinuation()
        {
            return Ok();
        }

        [HttpGet("delayed")]
        public IActionResult Delayed()
        {
            return Ok();
        }

        [HttpGet("recurring")]
        public IActionResult Recurring()
        {
            return Ok();
        }
    }
}
