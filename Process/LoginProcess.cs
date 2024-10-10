using Login_Registor.Data;
using Login_Registor.Model;
using Login_Registor.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Login_Registor.Providers.AccessProviders;

namespace Login_Registor.Process
{
    public class LoginProcess
    {
        public static async Task<ApiResponse> Login(AuthModel model)
        {
            var apiResponse = new ApiResponse { Status = (byte)StatusFlag.Failed };
            try
            {
                DefaultContext defaultContext = new();
                User user = await defaultContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == model.UserName && u.Password == EncryptionProviders.Encrypt(model.PassWord));
                if (user == null)
                {
                    apiResponse.Message = "Enter valid credentials"; return(apiResponse);
                }
                else
                {
                    user.Password = ""; DateTime expiry = DateTime.Now.Add(TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay);
                    Claim additionalClaim = new(ClaimTypes.Role, user.IsAdmin ? Convert.ToString(SystemUserType.Admin) : Convert.ToString(SystemUserType.User));
                    user.AccessToken = GetUserAccessToken(user, expiry, additionalClaim);
                    apiResponse.data = user; apiResponse.Status = (byte)StatusFlag.Success;
                    return(apiResponse);
                }
            }
            catch (Exception ex) { apiResponse.DetailedError = Convert.ToString(ex); return apiResponse;  }
        }
        public static async Task<ApiResponse> Register(User data)
        {
            ApiResponse apiResponse = new() { Status = (byte)StatusFlag.Failed };
            try
            {
                DefaultContext defaultcontext = new();
                if (defaultcontext.Users.Any(d => d.Username == data.Username)) { apiResponse.Message = "User already Exist";}
                else if(defaultcontext.Users.Any(d=> d.ContactNo == data.ContactNo)) { apiResponse.Message = "Contact number already exist"; }
                else
                {
                    data.IsAdmin = false;
                    data.Password = EncryptionProviders.Encrypt(data.Password);
                    await defaultcontext.Users.AddAsync(data);
                    await defaultcontext.SaveChangesAsync();
                    apiResponse.Message = "Register Successfull";apiResponse.Status = (byte)StatusFlag.Success;
                }
                await defaultcontext.SaveChangesAsync();
            }
            catch(Exception ex) { apiResponse.DetailedError = Convert.ToString(ex);}
            return apiResponse;
        }
    }
}
