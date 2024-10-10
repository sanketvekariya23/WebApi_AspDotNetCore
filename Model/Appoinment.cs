using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Login_Register.Model
{
    public class Appoinment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key] public int AppoinmentId { get; set; }
        [Required]public string PatientName{ get; set; }
        [ForeignKey("DoctorId")] public int DoctorId { get; set; }
        public string Description { get; set; }
        public DateTime AppoinmentDate { get; set; }
        public DateTime AppoinmentStartTime { get; set; }
        public DateTime AppoinmentEndTime { get; set; }
        public decimal TotalFees { get; set; }
        public decimal PaidFees { get; set; }
        [NotMapped]public decimal RemainingFees { get; set; }
        [NotMapped]public TimeOnly TotalAppoinmentTime { get; set; }
    }
}
