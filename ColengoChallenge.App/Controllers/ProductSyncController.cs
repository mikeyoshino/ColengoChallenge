using ColengoChallenge.Api.Features.Products;
using ColengoChallenge.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> SyncProducts()
        {
            try
            {
                var request = new GetDemoProductsRequest();
                request.Page = 1;
                request.PageSize = 50;
                await _productSyncService.SyncProductsAsync(request);
                return Ok("Products synchronized successfully.");
            } catch (Exception ex)
            {
                //log error here
                return BadRequest(ex.Message);
            }
        }
    }
}
