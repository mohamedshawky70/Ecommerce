using Ecommerce.Models;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOptionsController : ControllerBase
    {
        private readonly IRepo<ProductOptions> repo;

        public ProductOptionsController(IRepo<ProductOptions> repo)
        {
            this.repo = repo;
        }
    }
}
