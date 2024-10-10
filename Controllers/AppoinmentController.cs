using Login_Register.Model;
using Login_Register.Process;
using Login_Registor.Controllers;
using Login_Registor.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Login_Registor.Providers.AccessProviders;

namespace Login_Register.Controllers
{
    [Authorize(Roles = nameof(SystemUserType.User)),Route("api/[controller]")]
    public class AppoinmentController : BaseController
    {
        readonly AppoinmentProcess process;
        public AppoinmentController([FromServices] User user , ILogger<AppoinmentProcess> logger) { process = new AppoinmentProcess(logger) { CurrentAppoinment = user}; }
        [HttpGet]public async Task<IActionResult> Get() => SendResponse(await process.Get(),true);
        [HttpPost] public async Task<IActionResult> Post([FromBody] Appoinment appoinment ) => SendResponse(await process.Create(appoinment));
        [HttpDelete] public async Task<IActionResult> Put(int AppoinmentId) => SendResponse(await process.Delete(AppoinmentId));
    }
}
