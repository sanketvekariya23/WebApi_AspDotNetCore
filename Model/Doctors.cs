using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Model
{
    public class Doctors
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key] public int DoctorId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string MobileNumber { get; set; }
        [Required] public int Experience { get; set; }
        [Required] public string Speciality { get; set;}
        [Required] public int DoctorCode { get; set; }
    }
}
