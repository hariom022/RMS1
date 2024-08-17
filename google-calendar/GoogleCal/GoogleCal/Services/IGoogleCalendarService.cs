using Google.Apis.Calendar.v3.Data;

namespace GoogleCal.Services
{
    public interface IGoogleCalendarService
    {
        Task<Event> CreateEvent(Event request, CancellationToken cancellationToken);
    }
}
