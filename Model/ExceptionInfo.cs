using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Model
{
    public class ExceptionInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key] public int ExceptionId { get; set; }
        public string ExceptionName { get; set; }
        public string ExceptionInformation { get; set; }
        public string FileName { get; set; }
        public string MethodName { get; set; }
        public DateTime Time  { get; set; }
    }
}
