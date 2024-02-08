using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService ProductService;

        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }

        [JwtTokenAuthorization]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                productDto.Creator = User.Identity.Name;
                var succeed = await ProductService.CreateProductAsync(productDto);
                return Ok(succeed);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await ProductService.GetAllProductsAsync();
                return Ok(products);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await ProductService.GetProductByIdAsync(id);
                return product == null ? NotFound() : Ok(product);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [JwtTokenAuthorization]
        [ServiceFilter(typeof(ProductAuthorizationFilter))]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                productDto.Id = id;
                productDto.Creator = User.Identity.Name;
                var res = await ProductService.UpdateProductAsync(productDto);
                return Ok(res);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [JwtTokenAuthorization]
        [ServiceFilter(typeof(ProductAuthorizationFilter))]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingProduct = await ProductService.GetProductByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                await ProductService.DeleteProductAsync(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}