using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnoraApi.DTOs;
using TechnoraApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace TechnoraApi.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetAll().OrderBy(r => r.Name);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Product> GetProduct(int id)
        {
            Product product = _productRepository.GetBy(id);
            if (product == null) return NotFound();
            return product;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Product> PostProduct(ProductDTO product)
        {
            Product productToCreate = new Product() { Name = product.Name, Price = product.Price };
            foreach (var i in product.Specifications)
                productToCreate.AddSpecification(new Specification(i.description) { Type=i.Type});
            _productRepository.Add(productToCreate);
            _productRepository.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new { id = productToCreate.Id }, productToCreate);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _productRepository.Update(product);
            _productRepository.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public ActionResult<Product> DeleteProduct(int id)
        {
            Product product = _productRepository.GetBy(id);
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.Delete(product);
            _productRepository.SaveChanges();
            return product;
        }




        [HttpPost("{id}/specifications")]
        [AllowAnonymous]
        public ActionResult<Specification> PostSpecification(int id, SpecificationDTO specification)
        {
            if (!_productRepository.TryGetProduct(id, out var product))
            {
                return NotFound();
            }
            var specificationToCreate = new Specification(specification.description,specification.Type);
            product.AddSpecification(specificationToCreate);
            _productRepository.SaveChanges();
            return CreatedAtAction("GetSpecification", new { id = product.Id, specificationId = specificationToCreate.Id }, specificationToCreate);
        }
    }
}