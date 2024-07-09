using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Models
{
    public class GoodsIssue
    {
        public int Id { get; set; }
        [Required]
        public string QuotationNo { get; set; }
        [Required]
        public string OBDNo { get; set; }
        [Required]
        public string ProductNumber { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string StoreName { get; set; }
        public DateOnly DeliveredDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Status { get; set; }

        public int QuotationId { get; set; } // Foreign key
        [ForeignKey("QuotationId")]
        [ValidateNever]
        public PurchaseOrderHeader Quotation { get; set; }


    }
}
