using Login_Register.Model;
using Login_Registor.Data;
using Login_Registor.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Login_Register.Process
{
    public class EmployeeProcess : GlobelVeriable
    {
        private readonly ILogger<EmployeeProcess> logger;
        private readonly IMemoryCache memoryCache;
        public EmployeeProcess(ILogger<EmployeeProcess> _logger, IMemoryCache memoryCache) { logger = _logger; this.memoryCache = memoryCache; }
        public async Task<ApiResponse> Get()
        {
            ApiResponse apiresponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                const string cacheKey = "EmployeeList";
                if (!memoryCache.TryGetValue(cacheKey, out Employee[] employee))
                {
                    using DefaultContext defaultContext = new();
                    employee = await defaultContext.Employees.AsNoTracking().Include(x => x.EmployeeType).Include(x => x.Designation).ToArrayAsync();
                    if (employee != null || employee.Length > 0) 
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                        memoryCache.Set(cacheKey, employee, cacheEntryOptions);
                        apiresponse.data = employee;
                    }
                    else
                    {
                        apiresponse.Message = "No data found";
                        apiresponse.data = new Employee[] { };
                    }
                }
                else
                {
                    apiresponse.data = employee;
                }
                logger.LogInformation("Successfully retrieved employees.");
            }
            catch (Exception ex) { apiresponse.Status = (Byte)StatusFlag.Failed; apiresponse.DetailedError = Convert.ToString(ex); logger.LogError("Failed to retrieve employees.", apiresponse.DetailedError); LogInformation(ex,nameof(Get)); }
            return apiresponse;
        }
        public async Task<ApiResponse> GetJoinedCurrentMonth()
        {
            ApiResponse apiResponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultContext = new();
                int currentMonth = DateTime.Now.Month; int currentYear = DateTime.Now.Year;
                apiResponse.data = await defaultContext.Employees.AsNoTracking().Where(e => e.JoiningDate.Month == currentMonth && e.JoiningDate.Year == currentYear).ToListAsync();
                if (apiResponse.data == null) { apiResponse.Message = "Data not Found"; }
                logger.LogInformation("Successfully retrieved employees.");
            }
            catch (Exception ex){apiResponse.Status = (Byte)StatusFlag.Failed;apiResponse.DetailedError = Convert.ToString(ex); logger.LogInformation("Error occured while retrieved employees."); LogInformation(ex, nameof(GetJoinedCurrentMonth)); }
            return apiResponse;
        }
    
        public async Task<ApiResponse> Create(Employee employee)
        {
            const string cacheKey = "EmployeeList";
            ApiResponse apiResponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultcontext = new();
                employee.EmployeeType = null;
                employee.Designation = null;
                defaultcontext.Employees.Add(employee);
                await defaultcontext.SaveChangesAsync();
                memoryCache.Remove(cacheKey);
                logger.LogInformation("Creating employees Successfully.");
            }
            catch (Exception ex){apiResponse.Status = (Byte)StatusFlag.Failed;apiResponse.DetailedError = Convert.ToString(ex); logger.LogInformation("Creating Feild..."); LogInformation(ex, nameof(Create)); }
            return apiResponse;
        }
        public async Task<ApiResponse> Delete(int employeeid)
        {
            logger.LogInformation("Deleting employees.");
            ApiResponse apiResponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultcontext = new();
                Employee userexist = await defaultcontext.Employees.AsNoTracking().FirstOrDefaultAsync(d => d.EmployeeId == employeeid);
                if (userexist != null) { defaultcontext.Employees.Remove(userexist);}
                else { apiResponse.Message = "User not exist"; }
                await defaultcontext.SaveChangesAsync();
                logger.LogInformation("Deleting Successfully.");
            }
            catch (Exception ex) { apiResponse.Status = (Byte)StatusFlag.Failed; apiResponse.DetailedError = Convert.ToString(ex); logger.LogInformation("Deleting Feild."); LogInformation(ex, nameof(Delete)); }
            return apiResponse;
        }
        public async Task<ApiResponse> GetById(int employeeid)
        {
            logger.LogInformation("Get Employee by Id.");
            ApiResponse apiResponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defaultcontext = new();
                Employee employee =defaultcontext.Employees.AsNoTracking().FirstOrDefault(u => u.EmployeeId == employeeid);
                if (employee == null){apiResponse.Status = (Byte)StatusFlag.Failed;apiResponse.DetailedError = "Employee not found.";}
                else { apiResponse.data = employee; }
                logger.LogInformation("Getting employees Successfully.");
            }
            catch (Exception ex){ apiResponse.Status = (Byte)StatusFlag.Failed; apiResponse.DetailedError = Convert.ToString(ex); logger.LogInformation("Getting employees Feild."); LogInformation(ex, nameof(GetById)); }
            return apiResponse;
        }
        public async Task LogInformation(Exception ex , string methodname)
        {
            DefaultContext defaultContext = new();
            var exceptionInfo = new ExceptionInfo { ExceptionName = ex.GetType().Name,ExceptionInformation = ex.Message, FileName = nameof(EmployeeProcess), MethodName = methodname ,Time = DateTime.UtcNow};
            await defaultContext.ExceptionInfos.AddAsync(exceptionInfo);
            await defaultContext.SaveChangesAsync();
        }
    }
}
