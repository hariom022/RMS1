using Google.Apis.Services;
using GoogleCal.Services;
using GoogleCal.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<GoogleCalendarSettings>(builder.Configuration.GetSection(nameof(GoogleCalendarSettings)));
builder.Services.AddSingleton<IGoogleCalendarSettings>(x => x.GetRequiredService<IOptions<GoogleCalendarSettings>>().Value);

builder.Services.AddScoped<IGoogleCalendarService, GoogleCalendarService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
