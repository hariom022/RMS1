using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;
using RMS.Models;

namespace RMS.DataAccess.Repository
{
    public class ConsumptionEntryRepository : IConsumptionEntryRepository
    {
        private readonly ApplicationDbContext _db;
        public ConsumptionEntryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<Guid> AddConsumptionEntryAsync(ConsumptionEntryRepository consumptionEntry)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ConsumptionEntryRepository>> GetAllConsumptionEntryAsync()
        {

            throw new NotImplementedException();
        }

        public bool Save(ConsumptionEntry consumptionEntry, StoreStock storeStock)
        {
            var trans = _db.Database.BeginTransaction();
            try
            {
                storeStock.Quantity -= consumptionEntry.Quantity;

                _db.StoreStocks.Update(storeStock);
                _db.ConsumptionEntries.Add(consumptionEntry);
                _db.SaveChanges();
                trans.Commit();
                return true;

            }
            catch (Exception)
            {
                trans.Rollback();
                return false;
            }
        }

        
    }
}
