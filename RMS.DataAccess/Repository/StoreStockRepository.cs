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

    public class StoreStockRepository : Repository<StoreStock>, IStoreStockRepository
    {
        private readonly ApplicationDbContext _db;

        public StoreStockRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(StoreStock obj)
        {
            _db.StoreStocks.Update(obj);

        }
    }
}
