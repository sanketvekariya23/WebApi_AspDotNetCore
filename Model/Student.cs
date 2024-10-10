using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Model
{
    public class Student
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key]public int EnrollementNo { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string ContactNo { get; set; }
        [Required] public bool IsActive { get; set; } = true;
        [Required] public bool IsAdmin { get; set; }
    }
}
