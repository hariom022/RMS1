using RMS.Models;

namespace RMS.DataAccess.Repository.IRepository
{
    public interface IPurchaseOrderCartRepository : IRepository<PurchaseOrderCart>
    {
        void Update(PurchaseOrderCart obj);
        void Save();
    }
}
