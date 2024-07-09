using RMS.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models.ViewModels
{
    public class InvoiceVM
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<InvoiceDTO> InvoiceList { get; set; }
    }
}
