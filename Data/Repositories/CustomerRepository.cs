using Microsoft.EntityFrameworkCore;
using TechnoraApi.Models;
using System.Linq;

namespace TechnoraApi.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ProductContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(ProductContext dbContext)
        {
            _context = dbContext;
            _customers = dbContext.Customers;
        }

        public Customer GetBy(string email)
        {
            return _customers.Include(c => c.Items).ThenInclude(f => f.Product).ThenInclude(r => r.Specifications).SingleOrDefault(c => c.Email == email);
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}