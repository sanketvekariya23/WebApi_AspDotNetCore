using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Model
{
    public class Designation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key]public int DesignationId { get; set; }
        [Required]public string DesignationName { get; set; }
    }
}
