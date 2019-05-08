using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechnoraApi.DTOs
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }


        public IList<SpecificationDTO> Specifications { get; set; }
    }
}