using RMS.Models;

namespace RMS.DataAccess.Repository.IRepository
{
    public interface IPurchaseOrderHeaderRepository : IRepository<PurchaseOrderHeader>
    {
        void Update(PurchaseOrderHeader obj);
        void Save();
    }
}
