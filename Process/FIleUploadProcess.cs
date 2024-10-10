using Login_Registor.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Login_Register.Process
{
    public class FIleUploadProcess : GlobelVeriable
    {
        public async Task<ApiResponse> UploadFile(IFormFile file) 
        {
            ApiResponse apiresponse = new() { Status = (Byte)StatusFlag.Success };
            try
            {
                if (file == null || file.Length == 0) { apiresponse.Message = "No file uploaded"; }
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                var filepath = Path.Combine(uploadsFolderPath, file.FileName);
                if(!Directory.Exists(uploadsFolderPath)) { Directory.CreateDirectory(uploadsFolderPath); }
                using (var stream = new FileStream(filepath, FileMode.Create)){await file.CopyToAsync(stream);}
            }
            catch (Exception ex) { apiresponse.Status = (byte)StatusFlag.Failed; apiresponse.DetailedError = Convert.ToString(ex); }
            return apiresponse;
        }
        public async Task<ApiResponse> MultiFileUpload(List<IFormFile> file)
        {
            ApiResponse apiresponse = new() { Status = (Byte)StatusFlag.Success };
            var filePathList = new List<string>();
            try
            {
                if (file == null) { apiresponse.Message = "No file uploaded"; }
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolderPath)) { Directory.CreateDirectory(uploadsFolderPath); }
                foreach (var f in file)
                {
                    if (f.Length > 0)
                    {
                       var filepath = Path.Combine(uploadsFolderPath, f.FileName);
                       using (var stream = new FileStream(filepath, FileMode.Create)) { await f.CopyToAsync(stream); }
                       filePathList.Add(filepath);
                    }
                }
            }
            catch (Exception ex) { apiresponse.Status = (byte)StatusFlag.Failed; apiresponse.DetailedError = Convert.ToString(ex); }
            return apiresponse;
        }
    }
}
