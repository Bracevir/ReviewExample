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

builder.Services.Configure<IdentityOptions>(options =>
{
    // ��������� ��� �������
    options.Password.RequireDigit = true; // ��������� ���� �� ���� �����
    options.Password.RequireLowercase = true; // ��������� ���� �� ���� �������� �����
    options.Password.RequireUppercase = true; // ��������� ���� �� ���� ��������� �����
    options.Password.RequireNonAlphanumeric = true; // ��������� ���� �� ���� ����������� ������
    options.Password.RequiredLength = 6; // ����������� ����� ������
    options.Password.RequiredUniqueChars = 1; // ����������� ���������� ���������� ��������
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});


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
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
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
app.MapControllerRoute(
    name: "admin",
    pattern: "adminka",
    defaults: new { controller = "Admin", action = "Index" });

app.Run();

