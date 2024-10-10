using Login_Register.Process;
using Login_Registor.Controllers;
using Login_Registor.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Login_Registor.Providers.AccessProviders;
using Microsoft.Extensions.Options;
using Login_Register.Model;

namespace Login_Register.Controllers
{
    [Authorize(Roles =nameof(SystemUserType.User)),Route("api/[controller]")]
    public class OtpController : BaseController
    {
        readonly EmailProcess process;
        public OtpController([FromServices] User user, IOptions<EmailSetting> settings) { process = new EmailProcess(settings) { CurrentUser = user }; }
        [HttpPost]public async Task<IActionResult> SendOtp([FromBody] string email)
        {
            var otp = new Random().Next(100000, 999999).ToString(); var message = $"Your OTP is: {otp}";
            await process.SendEmail(email,otp,message);
            return Ok(new { Message = "OTP sent successfully." });
        }
    }
}