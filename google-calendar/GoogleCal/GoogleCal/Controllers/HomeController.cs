using Google.Apis.Calendar.v3.Data;
using GoogleCal.Models;
using GoogleCal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoogleCal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGoogleCalendarService _googleCalendarService;


        public HomeController(ILogger<HomeController> logger, IGoogleCalendarService googleCalendarService)
        {
            _logger = logger;
            _googleCalendarService = googleCalendarService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> AddEvent()
        {
            Event newEvent = new Event()
            {
                Summary = "Google I/O 2015",
                Location = "800 Howard St., San Francisco, CA 94103",
                Description = "A chance to hear more about Google's developer products.",
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Parse("2024-08-28T09:00:00-07:00"),
                    TimeZone = "America/Los_Angeles",
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Parse("2024-08-28T17:00:00-07:00"),
                    TimeZone = "America/Los_Angeles",
                },
                Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                Attendees = new EventAttendee[] {
        new EventAttendee() { Email = "lpage@example.com" },
        new EventAttendee() { Email = "sbrin@example.com" },
        new EventAttendee() { Email = "amolrogye33@gmail.com" },
    },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = false,
                    Overrides = new EventReminder[] {
            new EventReminder() { Method = "email", Minutes = 24 * 60 },
            new EventReminder() { Method = "sms", Minutes = 10 },
        }
                }
            };

            CancellationToken cancellationToken = new CancellationToken();

            await _googleCalendarService.CreateEvent(newEvent, cancellationToken);

            return Ok();
        }
    }
}
