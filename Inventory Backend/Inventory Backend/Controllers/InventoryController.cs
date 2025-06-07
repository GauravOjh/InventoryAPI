using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        public InventoryController()
        {

        }

        [HttpPost]
        public IActionResult SaveInventoryData()
        {
            return Ok("success");
        }
    }
}
