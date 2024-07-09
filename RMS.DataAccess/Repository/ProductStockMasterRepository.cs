using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.DataAccess.Repository
{
    public class ProductStockMasterRepository : Repository<ProductStockMaster>, IProductStockMasterRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductStockMasterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
           _db.SaveChanges();
        }

        public void Update(ProductStockMaster obj)
        {
            _db.ProductStockMaster.Update(obj);                             
        }
    }
}
