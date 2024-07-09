using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    /// <summary>
    /// Quotation Model
    /// </summary>
    public class Quotation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string QuotationNo { get; set; }
        [Required]
        public string RequestNo { get; set; }

        public int PurchaseOrderHeaderId { get; set; } // Foreign key
        [ForeignKey("PurchaseOrderHeaderId")]
        public PurchaseOrderHeader PurchaseOrderHeader { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }
}
