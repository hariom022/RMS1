using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMS.DataAccess.Repository
{
    public class PurchaseOrderHeaderRepository : Repository<PurchaseOrderHeader>, IPurchaseOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public PurchaseOrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(PurchaseOrderHeader obj)
        {
            //_db.PurchaseOrderHeaders.Update(obj);
        }
    }
}
