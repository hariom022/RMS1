using RMS.Models;

namespace RMS.DataAccess.Repository.IRepository
{
    public interface IPurchaseOrderItemRepository : IRepository<PurchaseOrderItem>
    {
        void Update(PurchaseOrderItem obj);
        void Save();
    }
}
