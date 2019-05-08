using System.Collections.Generic;

namespace TechnoraApi.Models
{
    public interface IProductRepository
    {
        Product GetBy(int id);
        bool TryGetProduct(int id, out Product product);
        IEnumerable<Product> GetAll();
        void Add(Product product);
        void Delete(Product product);
        void Update(Product product);
        void SaveChanges();
    }
}
