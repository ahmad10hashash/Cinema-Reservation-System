using CRS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession(o=>
{
    o.IdleTimeout = TimeSpan.FromSeconds(1800);
});
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CRSDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("constr"));
}
);
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<CRSDbContext>();

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
app.UseSession();
app.MapControllerRoute(
            name: "area",
            pattern: "{area:exists}/{controller=CPanel}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Slider}/{action=Index}/{id?}");

app.Run();
