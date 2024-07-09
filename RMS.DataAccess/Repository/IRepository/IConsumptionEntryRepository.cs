namespace RMS.DataAccess.Repository.IRepository
{
    public interface IConsumptionEntryRepository
    {
        Task<Guid> AddConsumptionEntryAsync(ConsumptionEntryRepository consumptionEntry);
        Task<IEnumerable<ConsumptionEntryRepository>> GetAllConsumptionEntryAsync();

        //Task<ConsumptionEntry?> GetConsumptionEntryByIdAsync(int ? id);

    }
}
