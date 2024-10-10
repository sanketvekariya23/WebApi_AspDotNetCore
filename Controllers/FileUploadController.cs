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
    public class FileUploadController :BaseController
    {
        readonly FIleUploadProcess _process;
        public FileUploadController(FIleUploadProcess process) { _process = process; }
        [HttpPost]public async Task<IActionResult> file(IFormFile file) => SendResponse(await _process.UploadFile(file),true);
        [HttpPost("MultiFileUpload")] public async Task<IActionResult> file(List<IFormFile> file) => SendResponse(await _process.MultiFileUpload(file), true);
    }
}