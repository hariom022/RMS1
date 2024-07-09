
using RMS.Models;

namespace RMS.DataAccess.Repository.IRepository
{
    public interface IStoreMasterRepository : IRepository<StoreMaster>
    {
        void Update(StoreMaster obj);
    }
}
