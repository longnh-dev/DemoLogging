using DemoLogging.Models;
using DemoLogging.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoLogging.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productService.GetProducts();
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            return await _productService.GetProduct(productId);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> CreateNewProduct([FromBody] Product product)
        {
            var newProduct = await _productService.CreateNewProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { ProductId = newProduct.Id }, newProduct);
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateProduct(int productId, [FromForm] Product product)
        {
            if (productId != product.Id)
            {
                return BadRequest();
            }
            await _productService.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var productDelete = await _productService.GetProduct(productId);
            if (productDelete == null)
            {
                return NotFound();
            }

            await _productService.DeleteProduct(productDelete.Id);
            return Ok();
        }
    }
}