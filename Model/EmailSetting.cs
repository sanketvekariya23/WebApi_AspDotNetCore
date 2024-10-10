using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Model
{
    public class EmailSetting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key]public int EmailSettingId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string DisplayName { get; set; }
        public int Port { get; set; }
    }
}
