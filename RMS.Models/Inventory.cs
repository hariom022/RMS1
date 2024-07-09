using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        // Foreign key to reference the product
        public int ProductId { get; set; }
        public ProductMaster Product { get; set; }

        // Foreign key to reference the plant
        public int PlantId { get; set; }
        public StoreMaster Plant { get; set; }

        public int AmountInStock { get; set; }
    }
}
