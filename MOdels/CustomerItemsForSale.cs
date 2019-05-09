﻿namespace TechnoraApi.Models
{
    public class CustomerItemsForSale
    {
        #region Properties
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public Customer Customer { get; set; }

        public Product Product { get; set; }
        #endregion
    }
}