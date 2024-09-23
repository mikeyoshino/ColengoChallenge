using ColengoChallenge.Api.Features.Products;
using ColengoChallenge.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColengoChallenge.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductSyncController : ControllerBase
    {
        private readonly IProductSyncService _productSyncService;

        public ProductSyncController(IProductSyncService productSyncService)
        {
            _productSyncService = productSyncService;
        }


        [HttpGet("start")]
        public async Task<IActionResult> SyncProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize must be greater than 0.");
            }

            try
            {
                var request = new GetDemoProductsRequest
                {
                    Page = page,
                    PageSize = pageSize
                };

                await _productSyncService.SyncProductsAsync(request);
                return Ok("Products synchronized successfully.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, "Bad Gateway: Failed to fetch products.");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Internal Server Error: Failed to save products.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

    }
}
