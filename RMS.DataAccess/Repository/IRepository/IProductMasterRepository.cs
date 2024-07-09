using RMS.Models;

namespace RMS.DataAccess.Repository.IRepository
{
    public interface IProductMasterRepository : IRepository<ProductMaster>
    {
        void Update(ProductMaster obj);
        void Save();
    }
}
