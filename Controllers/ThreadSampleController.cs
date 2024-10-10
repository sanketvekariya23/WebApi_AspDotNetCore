using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Login_Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartThread : ControllerBase
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ThreadSampleController : ControllerBase
        {
            [HttpGet("start-thread")]public IActionResult StartThread(){
                Thread thread = new Thread(new ThreadStart(PerformLongRunningOperation));
                thread.Start();
                return Ok("Thread started to perform long-running operation.");
            }
            private void PerformLongRunningOperation(){
                for (int i = 0; i <10; i++)
                {
                    Console.WriteLine($"Thread working... {i + 1}");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Thread has completed its work.");
            }
        }
    }
}
