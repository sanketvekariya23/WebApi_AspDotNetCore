using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_Register.Model
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key]public int ProductId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public decimal Price { get; set; }
    }
}
