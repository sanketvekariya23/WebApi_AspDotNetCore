using Login_Register.Interface;
using Login_Register.Model;
using Login_Registor.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Login_Register.Repository
{
    public class ProductRepository : IProduct<Product>
    {
        DefaultContext _context = new();
        public IEnumerable<Product> GetProducts()
        {
            return _context.Product.ToList();
        }

        public  Product Add(Product product)
        {
             _context.Product.AddAsync(product);
            _context.SaveChanges();
            return product;
        }

        public void Delete(Product product)
        {
           _context.Product.Remove(product);
            _context.SaveChanges();
        }

        public Product GetById(int id)
        {
           return _context.Product.Find(id);
        }

        public void Update(Product product)
        {
            _context.Product.Update(product);
            _context.SaveChanges();
        }
    }
}
