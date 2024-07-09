using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMS.DataAccess.Repository
{
    public class StoreMasterRepository : Repository<StoreMaster>, IStoreMasterRepository
    {
        private readonly ApplicationDbContext _db;
        public StoreMasterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(StoreMaster obj)
        {
            _db.StoresMaster.Update(obj);
        }
    }
}
