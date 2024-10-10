using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Model
{
    public class EmailRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key]public int EmailRequestId { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
    }
}
