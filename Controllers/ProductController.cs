using Login_Register.Interface;
using Login_Register.Model;
using Login_Register.Repository;
using Login_Registor.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Login_Registor.Providers.AccessProviders;

namespace Login_Register.Controllers
{
    [Authorize(Roles =nameof(SystemUserType.User)), Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct<Product> _product;
        public ProductController([FromServices] IProduct<Product> _product) { this._product = _product; }
        [HttpGet] public ActionResult<IEnumerable<Product>> GetProduct() { return _product.GetProducts().ToList(); }
        [HttpGet("{id}")] public ActionResult<Product> GetProduct(int id) { var productExist = _product.GetById(id); if (productExist == null) { return NotFound(); } return Ok(productExist); }
        [HttpPost] public ActionResult<Product> PostProduct([FromBody] Product product) {  var createProduct = _product.Add(product);  return createProduct; }
        [HttpPut] public ActionResult<Product> PutUpdate([FromBody] Product product) { if (product.ProductId == null) { return NotFound(); } var productexist = _product.GetById(product.ProductId); if (productexist != null) { _product.Update(product); return Ok("Update SuccessFully.."); } else { return Ok("Data not found"); } }
        [HttpDelete("{id}")] public ActionResult Delete(int id) { var product = _product.GetById(id);  if (product != null) { _product.Delete(product); return Ok("Deleted SuccessFully.."); } else { return Ok("Data not found"); }}
    }
}
