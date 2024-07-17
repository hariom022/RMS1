using Microsoft.EntityFrameworkCore;
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

        public StoreStock GetStoreStock(int storeId, int productId)
        {
            try
            {
                return _db.StoreStocks.Where(x => x.ProductId == productId && x.StoreId == storeId).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public dynamic GetStoreStockList(int storeId)
        {
            try
            {
                return  (from ss in _db.StoreStocks
                          join p in _db.Products on ss.ProductId equals p.ProductId
                          where ss.StoreId == storeId
                          select new
                          {
                              ss.ProductId,
                              ss.Quantity,
                              p.Number,
                              p.Description
                          });
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Update(StoreStock obj)
        {
            _db.StoreStocks.Update(obj);
        }

        public StoreStock GetStoreStock(int storeId, string productName)
        {
            try
            {
                return _db.StoreStocks.Where(x => x.StoreId == storeId &&
                x.ProductId == _db.Products.FirstOrDefault(y => y.Number == productName).ProductId
                ).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
