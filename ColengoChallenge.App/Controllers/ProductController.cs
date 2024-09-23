using Azure.Core;
using ColengoChallenge.Api.Features.Products;
using ColengoChallenge.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ColengoChallenge.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository ProductRepository;
        public ProductController(IProductRepository aProductRepository)
        {
            ProductRepository = aProductRepository;
        }

        // GET api/product/get-products?page=1&pageSize=10
        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductRequest request)
        {
            // Validate the request parameters
            if (request.Page <= 0)
            {
                return BadRequest();
            }

            if (request.PageSize <= 0)
            {
                return BadRequest();
            }

            // Fetch the products from the repository
            var response = await ProductRepository.GetProducts(request);

            // Return the response as JSON
            return Ok(response);
        }

    }
}
