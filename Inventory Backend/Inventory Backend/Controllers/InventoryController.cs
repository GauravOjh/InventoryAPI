using Inventory_Backend.AppDbContext;
using Inventory_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("addinventory")]
        public async Task<IActionResult> SaveInventoryData([FromBody] InventoryRequestData InventorydataDTO)
        {
            if (InventorydataDTO == null) return BadRequest();

            if (string.IsNullOrWhiteSpace(InventorydataDTO.productname) || InventorydataDTO.productname=="string")
                return BadRequest("Product name is required.");

            if (InventorydataDTO.AvailableQuantity < 0 || InventorydataDTO.ReOrderAmount < 0)
                return BadRequest("Quantities must be non-negative.");
            if (InventorydataDTO.productid <= 0){
                return BadRequest("No Product Id for 0 or Negative Values");
            }

            var product = new Inventorydata
            {
                productid = InventorydataDTO.productid,
                productname = InventorydataDTO.productname,
                AvailableQuantity = InventorydataDTO.AvailableQuantity,
                ReOrderAmount = InventorydataDTO.ReOrderAmount
            };

            _context.Inventory.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product created", productId = product.productid ,Success = true });
        }

        [HttpGet("getallinventorydata")]
        public async Task<ActionResult<IEnumerable<InventoryRequestData>>> GetAll()
        {
            var products = await _context.Inventory.OrderByDescending(x=>x.Id).ToListAsync();
            if(products is null)
            {
                return BadRequest();
            }
            return Ok(new {Data = products , Success = true});
        }

        [HttpGet("getinventorydata/{id}")]
        public async Task<ActionResult<InventoryRequestData>> GetById(int id)
        {
            if (id <= 0 || id == null)
                return BadRequest("Invalid product ID.");

            var product = await _context.Inventory.FirstOrDefaultAsync(x=>x.productid==id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }

        [HttpPut("inventorydataupdate/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InventoryRequestUpdateData inventoryRequestData)
        {
            if (id <= 0)
                return BadRequest("Invalid product ID.");

            if (inventoryRequestData == null)
                return BadRequest("Product data is required.");

            if (string.IsNullOrWhiteSpace(inventoryRequestData.productname))
                return BadRequest("Product name is required.");

            var product = await _context.Inventory.FirstOrDefaultAsync(x => x.productid == id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");
            if(product is not null)
            {
                product.productname = inventoryRequestData.productname;
                product.AvailableQuantity = inventoryRequestData.AvailableQuantity;
                product.ReOrderAmount = inventoryRequestData.ReOrderAmount;
                _context.Inventory.Update(product);
                await _context.SaveChangesAsync();
            }

            return Ok(new { message = "Product updated successfully", product });
        }

        [HttpDelete("deleteinventorydata/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid product ID.");

            var product = await _context.Inventory.FirstOrDefaultAsync(x => x.productid == id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            _context.Inventory.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully" });
        }
    }
}
