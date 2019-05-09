using Microsoft.AspNetCore.Identity;
using TechnoraApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoraApi.Data
{
    public class ProductDataInitializer
    {
        private readonly ProductContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductDataInitializer(ProductContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {       
                Customer customer = new Customer { Email = "technora@hogent.be", FirstName = "Adam", LastName = "Master" };
                _dbContext.Customers.Add(customer);
                await CreateUser(customer.Email, "P@ssword1111");
                Customer student = new Customer { Email = "student@hogent.be", FirstName = "Student", LastName = "Hogent" };
                _dbContext.Customers.Add(student);
                student.AddItemForSale(_dbContext.Products.First());
                await CreateUser(student.Email, "P@ssword1111");
                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}