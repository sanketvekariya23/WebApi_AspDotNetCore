using Login_Register.Model;

namespace Login_Registor.Model
{
    public struct ApiResponse
    {
        public byte Status { get; set; }
        public string Message { get; set; }
        public string DetailedError { get; set; }
        public Object data { get; set; }
    }
    public enum StatusFlag : byte
    {
        Success = 1,
        Failed = 2,
        AlredyExist = 3,
        DependencyExist = 4,
        NotPermitted = 5,
    }
    public struct AuthModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
    