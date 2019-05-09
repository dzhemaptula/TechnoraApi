using Microsoft.EntityFrameworkCore;
using TechnoraApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace TechnoraApi.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        private readonly DbSet<Product> _products;

        public ProductRepository(ProductContext dbContext)
        {
            _context = dbContext;
            _products = dbContext.Products;
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.Include(r => r.Specifications).ToList();
        }

        public Product GetBy(int id)
        {
            return _products.Include(r => r.Specifications).SingleOrDefault(r => r.Id == id);
        }

        public bool TryGetProduct(int id, out Product product)
        {
            product = _context.Products.Include(t => t.Specifications).FirstOrDefault(t => t.Id == id);
            return product != null;
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Update(product);
        }

        public void Delete(Product product)
        {
            _products.Remove(product);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}