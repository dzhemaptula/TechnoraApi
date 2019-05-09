using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TechnoraApi.Models
{
    public class Product
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public string Chef { get; set; }

        public ICollection<Specification> Specifications { get; private set; }
        #endregion

        #region Constructors
        public Product()
        {
            Specifications = new List<Specification>();
            Created = DateTime.Now;
        }

        public Product(string name) : this()
        {
            Name = name;
        }
        #endregion

        #region Methods
        public void AddSpecification(Specification specification) => Specifications.Add(specification);

        public Specification GetSpecification(int id) => Specifications.SingleOrDefault(i => i.Id == id);
        #endregion
    }
}