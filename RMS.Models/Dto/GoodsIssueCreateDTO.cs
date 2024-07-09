using System.ComponentModel.DataAnnotations;

namespace RMS.Models.Dto
{
    public class GoodsIssueCreateDTO
    {
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
    }
}
