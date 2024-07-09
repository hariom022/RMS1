using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        [Required]
        public string InvoiceNo { get; set; }
        [Required]
        public string OBDNo { get; set; }
        [Required]
        public string QuotationNo { get; set; }
        [Required]
        public string ProductNumber { get; set; }
        [Required]
        public string StoreName { get; set; }
        public DateOnly DeliveredDate { get; set; }
        public DateOnly CreatedDate { get; set; }
        public string? Status { get; set; }

        public int GoodsIssueId { get; set; } // Foreign key
        [ForeignKey("GoodsIssueId")]
        [ValidateNever]
        public GoodsIssue GoodsIssue { get; set; }



    }
}
