using Login_Register.Model;
using Login_Registor.Model;

namespace Login_Register.Process
{
    public class GlobelVeriable : IDisposable
    {
        public User CurrentStudent { get; set; }
        public User CurrentEmployee { get; set; }
        public User CurrentDoctor { get; set; } 
        public User CurrentAppoinment { get; set; }
        public User CurrentUser { get; set; }
        public void Dispose() { GC.SuppressFinalize(this); }
    }
}
