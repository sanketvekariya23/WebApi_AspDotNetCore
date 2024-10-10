using Login_Registor.Model;
using Login_Registor.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Login_Registor.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult SendResponse(ApiResponse apiResponse, bool showMessage = false)
        {
            if (showMessage) { apiResponse.Message ??= Convert.ToString(Enum.Parse<StatusFlag>(Convert.ToString(apiResponse.Status))).AddSpaceBeforeCapital(); }
            return apiResponse.Status == (byte)StatusFlag.Failed ? BadRequest(apiResponse) : Ok(apiResponse);
        }
    }
}
