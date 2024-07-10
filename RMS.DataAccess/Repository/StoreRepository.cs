using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMS.DataAccess.Repository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly ApplicationDbContext _db;
        public StoreRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Store obj)
        {
            _db.Stores.Update(obj);
        }
    }
}
