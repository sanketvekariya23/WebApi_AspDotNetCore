using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Model
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key] public int EmployeeId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public DateTime DOB { get; set; }
        [Required] public string EmailId { get; set; }
        [Required] public string ContactNumber { get; set; }
        [Required] public int EmployeeTypeId { get; set; }
        [Required] public int DesignationId { get; set; }
        [ForeignKey("EmployeeTypeId")]public EmployeeType EmployeeType { get; set; }
        [ForeignKey("DesignationId")] public Designation Designation { get; set; }
        public string Gender { get; set; }
        [Required]public DateTime JoiningDate { get; set; }
    }
}
