using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);


//Connection With Sql

var ConnectionString = builder.Configuration.GetConnectionString(name:"DefaultConnection") ?? 
    throw new InvalidOperationException("No Connection String Was Found");

builder.Services.AddDbContext<ApplicationDbcontext>(options =>
options.UseSqlServer(ConnectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();





// add servces
builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();
builder.Services.AddScoped<IDeviceService, DeviceServices>();
builder.Services.AddScoped<IGameServices, GameServices>();


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
