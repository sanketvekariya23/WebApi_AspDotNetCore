using Login_Register.Controllers;
using Login_Register.Interface;
using Login_Register.Model;
using Login_Registor.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Login_Register_UnitTest.Controller
{
    public class ProductControllerTest
    {
        private readonly ProductController _controller;
        private readonly DefaultContext _context;
        private readonly Mock<IProduct<Product>> _mockProduct;
        public ProductControllerTest()
        {
            _mockProduct = new Mock<IProduct<Product>>();
            _context = new DefaultContext();
            _controller = new ProductController(_mockProduct.Object);
        }
        [Fact]
        public async Task GetReturn_SuccessResponse()
        {
            var product = new List<Product> { new Product { ProductId = 1,Name="Test Product From unit Test",Price=15.00M } };
            _mockProduct.Setup(repo=>repo.GetProducts()).Returns(product);
            var result = _controller.GetProduct();
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Post_Create_ReturnsSuccessResponse()
        {
            var product = new Product { Name = "Test Product From unit Test", Price = 15.00M  };
            _mockProduct.Setup(repo=>repo.Add(product)).Returns(product);
            var result = _controller.PostProduct(product);
            //var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
        }
        [Fact]
        public async void PutUpdate_ReturnsOkResult_ForValidProduct()
        {
            var productTUpdate = new Product {ProductId=1, Name = "Product Inserted", Price = 500.00M };
            _mockProduct.Setup(repo => repo.GetById(productTUpdate.ProductId)).Returns(productTUpdate);
           var result = _controller.PutUpdate(productTUpdate);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Update SuccessFully..", okResult.Value);
            Assert.NotNull(result);
        }
        [Fact]
        public async void Delete_ReturnsSuccessResponse()
        {
            var existingProduct = new Product { ProductId = 1, Price = 15.00M };
            _mockProduct.Setup(repo => repo.GetById(existingProduct.ProductId)).Returns(existingProduct);
            var result = _controller.Delete(existingProduct.ProductId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Deleted SuccessFully..", okResult.Value);
        }
    }
}
