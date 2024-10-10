using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Login_Registor.Model
{
    public class User
    {
        [Key] public int Id { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        [Required] public bool IsActive { get; set; } = true;
        [Required] public bool IsAdmin { get; set; }
        [NotMapped] public string AccessToken { get; set; }
    }
}
