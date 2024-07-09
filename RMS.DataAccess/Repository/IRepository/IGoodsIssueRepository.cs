using RMS.Models;

namespace RMS.DataAccess.Repository.IRepository
{
    public interface IGoodsIssueRepository : IRepository<GoodsIssue>
    {
        void Update(GoodsIssue obj);
        void Save();
    }
}
