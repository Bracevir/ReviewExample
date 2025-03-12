using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReviewExample.Models;
using ReviewExample.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ������������ EF Core � SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ��������� Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ����������� � �������������
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    try
    {
        var services = scope.ServiceProvider;
        await SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // ������� �������� - ����

app.Run();

