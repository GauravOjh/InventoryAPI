namespace Inventory_Backend.Models
{
    public class InventoryRequestUpdateData
    {
        public string productname { get; set; } = string.Empty;
        public int AvailableQuantity { get; set; }
        public int ReOrderAmount { get; set; }
    }
}
