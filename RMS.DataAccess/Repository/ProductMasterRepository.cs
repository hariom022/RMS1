using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMS.DataAccess.Repository
{
    public class ProductMasterRepository : Repository<ProductMaster>, IProductMasterRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductMasterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ProductMaster obj)
        {
            _db.ProductsMaster.Update(obj);
        }
    }
}
