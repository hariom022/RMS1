using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class StoreStock
    {
        public int StoreId { get; set; }
        public StoreMaster StoreMaster { get; set; }
        
        public int ProductId { get; set; }
        public ProductMaster ProductMaster { get; set; }

        public int Quantity { get; set; }
        public string? DataSource { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
