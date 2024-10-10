using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Login_Register.Model
{
    public class EmployeeType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [Key] public int EmployeeTypeId { get; set; }
        [Required]  public string  EmployeeTypeName { get; set; }
    }
}
