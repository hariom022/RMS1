using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        // Foreign key to reference the product
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Foreign key to reference the plant
        public int PlantId { get; set; }
        public Store Plant { get; set; }

        public int AmountInStock { get; set; }
    }
}
