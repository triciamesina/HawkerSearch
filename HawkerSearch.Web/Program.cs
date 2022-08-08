using HawkerSearch.Domain;
using HawkerSearch.Web.Interfaces;
using HawkerSearch.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<AppSettings>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor()
    .AddTransient<ILocationService, LocationService>()
    .AddScoped<IHawkerRepository, HawkerRepository>()
    .AddDbContext<HawkerContext>(options =>
        options.UseSqlServer(configuration.ConnectionString, x => x.UseNetTopologySuite()))
    .AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30))
    .AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
