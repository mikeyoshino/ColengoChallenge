using ColengoChallenge.Api.Features.Products;
using ColengoChallenge.Domain.Interfaces;
using ColengoChallenge.Infrastructure.Repository;
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

        // GET api/product?page=1&pageSize=10
        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var request = new GetProductRequest
            {
                Page = page,
                PageSize = pageSize
            };

            // Fetch the products from the repository
            var response = await ProductRepository.GetProducts(request);

            // Return the response as JSON
            return Ok(response);
        }



    }
}
