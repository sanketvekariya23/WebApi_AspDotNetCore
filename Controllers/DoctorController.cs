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
    [Authorize(Roles = nameof(SystemUserType.User)), Route("api/[controller]")]
    public class DoctorController : BaseController
    {
        readonly DoctorProcess process;
        readonly IMemoryCache memoryCache;
        public DoctorController([FromServices] User user, ILogger<DoctorProcess> logger, IMemoryCache memoryCache) { process = new DoctorProcess(logger,memoryCache) { CurrentDoctor = user }; this.memoryCache = memoryCache; }
        [HttpGet] public async Task<IActionResult> Get() => SendResponse(await process.Get(), true);
        [HttpPost] public async Task<IActionResult> Post([FromBody] Doctors doctor) => SendResponse(await process.Create(doctor), true);
    }
}
