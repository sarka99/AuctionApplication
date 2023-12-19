using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AuctionApplication.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AuctionApplication.Data;
using AuctionApplication.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddDbContext<AuctionDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDbConnection")));

builder.Services.AddDbContext<AuctionApplicationIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionApplicationIdentityContextConnection")));
builder.Services.AddDefaultIdentity<AuctionApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuctionApplicationIdentityContext>();

builder.Services.AddScoped<IAuctionPersistence, AuctionSqlPersistence>();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
