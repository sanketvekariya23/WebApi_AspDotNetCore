using Login_Register.Model;
using Login_Register.Repository;
using Login_Registor.Data;
using Moq;
using Xunit;

namespace Login_Register_UnitTest.Process
{
    public class ProductRepositoryTest
    {
        private readonly DefaultContext _context;
        private readonly ProductRepository _repository;
        public ProductRepositoryTest()
        {
            _context = new DefaultContext();
            _repository = new ProductRepository();
            _context.Database.EnsureCreated();
        }
        [Fact]
        public async Task Get_productReturnAllProducts()
        {
            var ProductToAdd = new List<Product>{new Product {Name= "Test Product From unitTest", Price =15.00M }};
            _context.Product.AddRange(ProductToAdd);
            _context.SaveChanges();
            IEnumerable<Product> products;
            products = await Task.FromResult(_repository.GetProducts());

            var addedProduct = products.FirstOrDefault(e=>e.Name =="Test Product From unitTest");
            Assert.NotNull(addedProduct);
            Assert.Equal("Test Product From unitTest", addedProduct.Name);
        } 
        [Fact]
        public async Task Create_Products()
        {
            var productToAdd = new Product { Name = "Product Add by UnitTest", Price = 1000.00M };
            var result =  _repository.Add(productToAdd);
            Assert.NotNull(result);
            Assert.Equal(productToAdd.Name , result.Name);
            Assert.Equal(productToAdd.Price, result.Price);
        }
        [Fact]
        public async Task Update_Products()
        {
            var productToAdd = new Product { Name = "Product Inserted", Price = 500.00M };
            _context.Product.Add(productToAdd);await _context.SaveChangesAsync();
            var productId = productToAdd.ProductId;
            var existingProduct = await _context.Product.FindAsync(productId);
            existingProduct.Name = "Product Updated";existingProduct.Price = 502.00M;
            _context.Product.Update(existingProduct);await _context.SaveChangesAsync();
            var updatedProduct = await _context.Product.FindAsync(productId);
            Assert.NotNull(updatedProduct);
            Assert.Equal("Product Updated", updatedProduct.Name);
            Assert.Equal(502.00M, updatedProduct.Price);
        }
        [Fact]
        public async Task Delete_Products()
        {
            var productToAdd = new Product { Name = "Product Inserted", Price = 500.00M };
            await _context.Product.AddAsync(productToAdd); await _context.SaveChangesAsync();
            _repository.Delete(productToAdd);
            var productdeleted = await _context.Product.FindAsync(productToAdd.ProductId);
            Assert.NotNull(productdeleted);
        }
    }
}
