using RMS.Models;

namespace RMS.DataAccess.Repository.IRepository
{
    public interface IQuotationRepository : IRepository<Quotation>
    {
        void Update(Quotation obj);
        void Save();
    }
}
