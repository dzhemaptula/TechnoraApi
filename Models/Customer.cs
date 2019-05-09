using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace TechnoraApi.Models
{
    public class Customer
    {
        #region Properties
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<CustomerItemsForSale> Items { get; private set; }

        public IEnumerable<Product> ItemsForSale => Items.Select(f => f.Product);
        #endregion

        #region Constructors
        public Customer()
        {
            Items = new List<CustomerItemsForSale>();
        }
        #endregion

        #region Methods
        public void AddItemForSale(Product product)
        {
            Items.Add(new CustomerItemsForSale() { ProductId = product.Id, CustomerId = CustomerId, Product = product, Customer = this });
        }
        #endregion
    }
}