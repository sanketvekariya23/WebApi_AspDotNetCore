using Login_Register.Model;

namespace Login_Register.Interface
{
    public interface IProduct<Product> where Product : class
    {
        IEnumerable<Product> GetProducts();
        Product Add(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        Product GetById(int id);  
    }
}
