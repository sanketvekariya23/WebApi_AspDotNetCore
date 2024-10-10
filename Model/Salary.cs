using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Model
{
    public class Salary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key] public int SalaryId { get; set; }
        [ForeignKey("EmployeeId")]public Employee Employee { get; set; }  
        [Required]public string Amount { get; set; }
    }
}
