using Hangfire;
using HangfireDemo.Services;
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
            BackgroundJob.Enqueue(() => Fail());

            return Ok();
        }

        [HttpGet("fire-forget2")]
        public IActionResult FireForget2([FromServices] IBackgroundJobClient client)
        {
            client.Enqueue<IReplicationService>(us => us.ReplicateAll());

            return Ok();
        }

        [AutomaticRetry(Attempts = 3)]
        public static void Fail()
        {
            throw new Exception("erro inesperado.");
        }

        [HttpGet("fire-forget-continuation")]
        public IActionResult FireForgetContinuation()
        {
            var id = BackgroundJob.Enqueue(() => Console.WriteLine("Atualizando dados"));

            var id2 = BackgroundJob.ContinueJobWith(id, () => Console.WriteLine("realizando backup"));

            BackgroundJob.ContinueJobWith(id2, () => Console.WriteLine("salvando backup"));

            return Ok();
        }

        [HttpGet("delayed")]
        public IActionResult Delayed()
        {
            BackgroundJob.Schedule(() => Console.WriteLine("Job agendado"), TimeSpan.FromSeconds(15));

            return Ok();
        }

        [HttpGet("recurring")]
        public IActionResult Recurring()
        {
            RecurringJob.AddOrUpdate("replications", () => Replicate(), "*/15 * * * * *");

            return Ok();
        }

        public static void Replicate() 
        {
            var id = BackgroundJob.Enqueue(() => Console.WriteLine("Atualizando dados"));

            var id2 = BackgroundJob.ContinueJobWith(id, () => Console.WriteLine("realizando backup"));

            BackgroundJob.ContinueJobWith(id2, () => Console.WriteLine("salvando backup"));
        }

    }
}
