using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMS.DataAccess.Repository
{
    public class PurchaseOrderCartRepository : Repository<PurchaseOrderCart>, IPurchaseOrderCartRepository
    {
        private readonly ApplicationDbContext _db;
        public PurchaseOrderCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(PurchaseOrderCart obj)
        {
            _db.PurchaseOrderCarts.Update(obj);
        }
    }
}
