using System.ComponentModel.DataAnnotations;

namespace Inventory_Backend.Models
{
    public class Inventorydata
    {
        [Key]
        public int Id { get; set; }
        public int productid { get; set; }
        public string productname { get; set; } = string.Empty;
        public int AvailableQuantity { get; set; }
        public int ReOrderAmount { get; set; }
    }
}
