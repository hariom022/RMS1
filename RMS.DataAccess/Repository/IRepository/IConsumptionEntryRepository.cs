using RMS.Models;

namespace RMS.DataAccess.Repository.IRepository
{
    public interface IConsumptionEntryRepository
    {
        Task<Guid> AddConsumptionEntryAsync(ConsumptionEntryRepository consumptionEntry);
        Task<IEnumerable<ConsumptionEntryRepository>> GetAllConsumptionEntryAsync();

        bool Save(ConsumptionEntry consumptionEntry, StoreStock storeStock);

        //Task<ConsumptionEntry?> GetConsumptionEntryByIdAsync(int ? id);

    }
}
