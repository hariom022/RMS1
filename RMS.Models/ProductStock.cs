using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class ProductStock
    {
        [Key]
        public int ProductStockId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Required]
        public int MinStock { get; set; }
        [Required]
        public int MaxStock { get; set; }

        public int? ReorderPoint { get; set; }
        public string? DataSource { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

    }
}
