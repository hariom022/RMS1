
using System.ComponentModel.DataAnnotations;


namespace RMS.Models.Dto
{
    public class InvoiceCreateDTO
    {
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
    }
}
