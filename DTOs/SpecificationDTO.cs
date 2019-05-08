using System.ComponentModel.DataAnnotations;

namespace TechnoraApi.DTOs
{
    public class SpecificationDTO
    {
        [Required]
        public string description { get; set; }
    }
}