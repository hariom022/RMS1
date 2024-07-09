using System.ComponentModel.DataAnnotations;

namespace RMS.Models.Dto
{
    public class QuotationCreateDTO
    {
        [Required]
        public string QuotationNo { get; set; }
        [Required]
        public string RequestNo { get; set; }
    }
}
