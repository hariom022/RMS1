using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMS.DataAccess.Repository
{
    public class QuotationRepository : Repository<Quotation>, IQuotationRepository
    {
        private readonly ApplicationDbContext _db;
        public QuotationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Quotation obj)
        {
            _db.Quotations.Update(obj);
        }
    }
}
