using System.ComponentModel.DataAnnotations;

namespace RMS.Models.Dto
{
    public class GoodsIssueProductDTO
    {
        [Required]
        public string ProductNumber { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
