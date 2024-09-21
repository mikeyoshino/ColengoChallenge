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

        [HttpGet]
        public string Index()
        {
            return "555";
        }



    }
}
