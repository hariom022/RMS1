using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMS.DataAccess.Repository
{
    public class GoodsIssueRepository : Repository<GoodsIssue>, IGoodsIssueRepository
    {
        private readonly ApplicationDbContext _db;
        public GoodsIssueRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(GoodsIssue obj)
        {
            _db.GoodsIssues.Update(obj);
        }
    }
}
