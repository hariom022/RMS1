using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Models
{
    public class StockConsumption
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public int AvailableQty { get; set; }
        public int ConsumedQty { get; set; }

    }
}
