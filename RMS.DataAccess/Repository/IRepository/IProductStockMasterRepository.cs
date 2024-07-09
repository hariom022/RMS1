using RMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.DataAccess.Repository.IRepository
{
    public interface IProductStockMasterRepository : IRepository<ProductStockMaster>
    {
        void Update(ProductStockMaster obj);
        void Save();
    }
}
