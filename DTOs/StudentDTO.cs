using System.ComponentModel.DataAnnotations;

namespace Login_Register.DTOs
{
    public class StudentDTO
    {
        [Key] public int EnrollementNo { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string ContactNo { get; set; }
        [Required] public bool IsActive { get; set; } = true;
        [Required] public bool IsAdmin { get; set; }
    }
}
