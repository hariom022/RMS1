using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    /// <summary>
    /// Domain Model for ConsumptionEntry
    /// </summary>
    public class ConsumptionEntry
    {
        [Key]
        public int ConsumptionId { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateOnly ConsumptionDate { get; set; }
        [Required]
        public string DataSource { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set;}
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set;}
    }
}
