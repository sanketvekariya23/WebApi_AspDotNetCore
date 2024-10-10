using AutoMapper;
using Login_Register.DTOs;
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
    [Authorize(Roles = nameof(SystemUserType.User)), Route("api/[controller]")]
    public class StudentController : BaseController
    {
        public StudentProcess process;
        public StudentController([FromServices] User user, ILogger<StudentProcess> logger, IMapper mapper) { process = new StudentProcess(logger, mapper) { CurrentStudent = user };}
        [HttpGet] public async Task<IActionResult> Get() => SendResponse(await process.Get(), true);
        [HttpPost] public async Task<IActionResult> Post([FromBody]StudentDTO data) => SendResponse(await process.Create(data),true);
        [HttpPut] public async Task<IActionResult> Put([FromBody]StudentDTO data)=> SendResponse(await process.Update(data),true);
        [HttpDelete] public async Task<IActionResult> Put(int enrollementno) => SendResponse(await process.Delete(enrollementno),true);
    }
}
