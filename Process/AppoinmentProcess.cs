using Login_Register.Model;
using Login_Registor.Data;
using Login_Registor.Model;
using Microsoft.EntityFrameworkCore;

namespace Login_Register.Process
{
    public class AppoinmentProcess :GlobelVeriable
    {
        readonly ILogger<AppoinmentProcess> _logger;
        public AppoinmentProcess(ILogger<AppoinmentProcess> logger) { _logger = logger; }
        public async Task<ApiResponse> Get()
        {
            ApiResponse apiResponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                var appointments = await defaultContext.Appoinment.AsNoTracking().ToListAsync();
                foreach (var appointment in appointments)
                {
                    appointment.RemainingFees = appointment.TotalFees - appointment.PaidFees;
                    appointment.TotalAppoinmentTime = TimeOnly.FromTimeSpan(appointment.AppoinmentEndTime.TimeOfDay - appointment.AppoinmentStartTime.TimeOfDay);
                }
                apiResponse.data = appointments;
                _logger.LogInformation("Appoinment information Retrieving Successfully.");
            }
            catch (Exception ex) { apiResponse.Status = (byte)StatusFlag.Failed; apiResponse.DetailedError = Convert.ToString(ex); LoggerExceptionInfo(ex, nameof(Get)); _logger.LogInformation("getting Appoinment failed.."); }
            return apiResponse;
        }
        public async Task<ApiResponse> Create(Appoinment appoinment)
        {
            ApiResponse apiResponse = new() { Status = (byte)StatusFlag.Success };
            try
            {   DefaultContext defaultContext = new();
                var appoinmentExist = await defaultContext.Appoinment.AsNoTracking().FirstOrDefaultAsync(u=>u.AppoinmentId == appoinment.AppoinmentId);
                if(appoinmentExist == null) {  await defaultContext.Appoinment.AddAsync(appoinment);  await defaultContext.SaveChangesAsync();  }
                else { apiResponse.Message = "Appoinment already exist"; }
            }catch(Exception ex) { apiResponse.Status = (byte)StatusFlag.Failed; apiResponse.DetailedError = Convert.ToString(ex); LoggerExceptionInfo(ex, nameof(Create)); _logger.LogInformation("Creating Appoinment failed.."); }
            return apiResponse;
        }
        public async Task<ApiResponse> Delete(int appoinmentId)
        {
            ApiResponse apiResponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultcontext = new();
                Appoinment userexist = await defaultcontext.Appoinment.AsNoTracking().FirstOrDefaultAsync(u => u.AppoinmentId == appoinmentId);
                if (userexist != null) {defaultcontext.Appoinment.Remove(userexist); }
                else { apiResponse.Message = "User not exist"; }
                await defaultcontext.SaveChangesAsync();
            }
            catch (Exception ex) { apiResponse.Status = (Byte)StatusFlag.Failed; apiResponse.DetailedError = Convert.ToString(ex);}
            return apiResponse;
        }
        public async Task LoggerExceptionInfo(Exception ex, string MethodName)
        {
            DefaultContext defaultContext = new();
            var ExceptionData = new ExceptionInfo { ExceptionName = ex.GetType().Name,ExceptionInformation = ex.Message,FileName = nameof(AppoinmentProcess), MethodName=MethodName , Time=DateTime.UtcNow};
        }
    }
}
