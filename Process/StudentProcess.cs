using Login_Register.Model;
using Login_Registor.Data;
using Login_Registor.Model;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Login_Register.DTOs;

namespace Login_Register.Process
{
    public class StudentProcess : GlobelVeriable
    {
        private readonly ILogger<StudentProcess> _logger;
        private readonly IMapper _mapper;
        public StudentProcess(ILogger<StudentProcess> logger , IMapper mapper){_logger = logger; _mapper = mapper; }
        public virtual async Task<ApiResponse> Get()
        {
            _logger.LogInformation("Retrieving Student information.");
            ApiResponse apiresponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                using DefaultContext defaultContext = new();
                var student = await defaultContext.Student.AsNoTracking().ToListAsync();
                apiresponse.data = _mapper.Map<List<StudentDTO>>(student);
                _logger.LogInformation("Retrieving Successfully.");
            }
            catch (Exception ex) { apiresponse.Status = (Byte)StatusFlag.Failed; apiresponse.DetailedError = Convert.ToString(ex); _logger.LogInformation("Retreving Feild."); LogExceptionAsync(ex , nameof(Get)); }
            return apiresponse;
        }
        public virtual async Task<ApiResponse> Create(StudentDTO data)
        {
            ApiResponse apiResponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                using DefaultContext defaultContext = new();
                var userexist = await defaultContext.Student.AsNoTracking().AnyAsync(s=>s.EnrollementNo == data.EnrollementNo);
                if(!userexist)
                {
                    var studententity = _mapper.Map<Student>(data);
                    await defaultContext.Student.AddAsync(studententity); await defaultContext.SaveChangesAsync();
                    _logger.LogInformation("Added Student Successfully.");
                }
            }
            catch (Exception ex) { apiResponse.Status = (Byte)StatusFlag.Failed; apiResponse.DetailedError = Convert.ToString(ex); _logger.LogInformation("Adding Student record failed."); LogExceptionAsync(ex, nameof(Create)); }
            return apiResponse;
        }
        public virtual async Task<ApiResponse> Update(StudentDTO data)
        {
            ApiResponse apiResponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                using DefaultContext defaultContext = new();
                var userexist = await defaultContext.Student.FindAsync(data.EnrollementNo);   
                if (userexist != null) 
                {
                    userexist.Name = data.Name;
                    userexist.ContactNo = data.ContactNo;
                    userexist.IsActive = data.IsActive;
                    userexist.IsAdmin = data.IsAdmin;
                    await defaultContext.SaveChangesAsync();
                    var studententity = _mapper.Map<Student>(data);
                    _logger.LogInformation("Update Student Successfully.");
                }
                else
                {
                    apiResponse.Status= (Byte)StatusFlag.Failed;
                    apiResponse.Message = "Data Not Found";
                }
            }
            catch(Exception ex) { apiResponse.Status = (Byte)StatusFlag.Failed; apiResponse.DetailedError = Convert.ToString(ex); _logger.LogInformation("Update Student record failed."); LogExceptionAsync(ex, nameof(Update)); }
            return apiResponse;
        }
        public virtual async Task<ApiResponse> Delete(int enrollementno)
        {
            ApiResponse apiresponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                DefaultContext defualtcontext = new();
                Student std = await defualtcontext.Student.AsNoTracking().FirstOrDefaultAsync(d=> d.EnrollementNo == enrollementno);
                if(std != null) { defualtcontext.Student.Remove(std); await defualtcontext.SaveChangesAsync(); _logger.LogInformation("Delete successfully."); }
                else { apiresponse.Message = "User Not Found"; }
            }
            catch (Exception ex) { apiresponse.Status = (Byte)StatusFlag.Failed;apiresponse.DetailedError = Convert.ToString(ex); _logger.LogInformation("Deleting Student Failed."); LogExceptionAsync(ex, nameof(Delete)); }
            return apiresponse;
        }
        public async Task LogExceptionAsync (Exception ex, string MethodName)
        {
            DefaultContext defualtcontext = new();
            var exceptionInfo = new ExceptionInfo { ExceptionName = ex.GetType().Name, ExceptionInformation = ex.Message, FileName = nameof(StudentProcess),MethodName = MethodName,Time=DateTime.UtcNow}; 
            await defualtcontext.ExceptionInfos.AddAsync(exceptionInfo);
            await defualtcontext.SaveChangesAsync();
        }
    }
}