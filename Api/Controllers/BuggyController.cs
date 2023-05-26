using Infrastraction.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDbContext _storeContext;

        public BuggyController(StoreDbContext storeContext)
        {
            _storeContext = storeContext;
        }

   
    }
}
