using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.Dto
{
    public class InvoiceDTO
    {
        public string QuotationNo { get; set; }
        [Required]
        public string ProductNumber { get; set; }
    }
}
