using Login_Register.Model;
using Login_Registor.Data;
using Login_Registor.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace Login_Register.Process
{
    public class DoctorProcess : GlobelVeriable
    {
        private readonly ILogger _logger;
        private readonly IMemoryCache memoryCache;
        public DoctorProcess(ILogger<DoctorProcess> logger, IMemoryCache memoryCache) { _logger = logger; this.memoryCache = memoryCache; }
        public async Task<ApiResponse> Get()
        {
            ApiResponse apiresponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                const string cacheKey = "doctorlist";
                if (!memoryCache.TryGetValue(cacheKey, out Doctors[] doctors))
                {
                    DefaultContext defualtcontext = new();
                    doctors = await defualtcontext.Doctors.AsNoTracking().ToArrayAsync();
                    if (doctors != null || doctors.Length>0)
                    {
                        var chacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                        memoryCache.Set(cacheKey, doctors,chacheEntryOptions);
                        apiresponse.data = doctors;
                    }
                    else
                    {
                        apiresponse.Message = " No Data Found.";
                        apiresponse.data = new Doctors[]{ };
                    }
                }
                else
                {
                    apiresponse.data = doctors;
                }
                _logger.LogInformation("Doctor information Retrieving Successfully.");
            }
            catch (Exception ex) { apiresponse.Status = (Byte)StatusFlag.Failed; _logger.LogInformation("Retrieving Failed."); apiresponse.DetailedError= Convert.ToString(ex.Message); LogExceptionAsync(ex, nameof(Get)); }
            return apiresponse;
        }
        public async Task<ApiResponse> Create(Doctors doctor)
        {
            ApiResponse apiResponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                const string cacheKey = "doctorlist";
                DefaultContext defualtcontext = new();
                await defualtcontext.Doctors.AddAsync(doctor);
                await defualtcontext.SaveChangesAsync();
                memoryCache.Remove("doctorlist"); 

            }catch(Exception ex) { apiResponse.Status = (byte)StatusFlag.Failed; apiResponse.DetailedError = Convert.ToString(ex); _logger.LogInformation("Creating Failed."); LogExceptionAsync(ex, nameof(Create)); }
            return apiResponse;
        }
        public async Task LogExceptionAsync(Exception ex, string MethodName)
        {
            DefaultContext defualtcontext = new();
            var exceptionInfo = new ExceptionInfo { ExceptionName = ex.GetType().Name, ExceptionInformation = ex.Message, FileName = nameof(DoctorProcess), MethodName = MethodName, Time = DateTime.UtcNow };
            await defualtcontext.ExceptionInfos.AddAsync(exceptionInfo);
            await defualtcontext.SaveChangesAsync();
        }
    }
}
