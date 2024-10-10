using Login_Register.Model;
using Login_Register.Process;
using Login_Registor.Controllers;
using Login_Registor.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static Login_Registor.Providers.AccessProviders;

namespace Login_Register.Controllers
{
    [Authorize(Roles = nameof(SystemUserType.User)),Route("api/[controller]")]
    public class EmployeeController : BaseController
    {
        readonly EmployeeProcess process;
        readonly IMemoryCache memoryChache;
        public EmployeeController([FromServices] User user, ILogger<EmployeeProcess> logger,IMemoryCache memoryCache){ process = new EmployeeProcess(logger,memoryCache) { CurrentEmployee = user }; this.memoryChache = memoryChache; }
        [HttpGet] public async Task<IActionResult> Get() =>SendResponse(await process.Get(), true);
        [HttpGet("GetById")] public async Task<IActionResult> GetById(int enrollementno) => SendResponse( await process.GetById(enrollementno),true);
        [HttpGet("joined-current-month")] public async Task<IActionResult> GetJoinedCurrentMonth() => SendResponse(await process.GetJoinedCurrentMonth(), true);
        [HttpPost] public async Task<IActionResult> Post([FromBody] Employee employee) => SendResponse(await process.Create(employee));
        [HttpDelete] public async Task<IActionResult> Put(int employeeid) => SendResponse(await process.Delete(employeeid));
    }
}
