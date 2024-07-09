using RMS.DataAccess.Data;
using RMS.DataAccess.Repository.IRepository;

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
    }
}
