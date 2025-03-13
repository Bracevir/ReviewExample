using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReviewExample.Models;
using ReviewExample.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация EF Core с SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Настройка Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Настройки для паролей
    options.Password.RequireDigit = true; // Требовать хотя бы одну цифру
    options.Password.RequireLowercase = true; // Требовать хотя бы одну строчную букву
    options.Password.RequireUppercase = true; // Требовать хотя бы одну заглавную букву
    options.Password.RequireNonAlphanumeric = true; // Требовать хотя бы один специальный символ
    options.Password.RequiredLength = 6; // Минимальная длина пароля
    options.Password.RequiredUniqueChars = 1; // Минимальное количество уникальных символов
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});


// Контроллеры и представления
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
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Главная страница - вход
app.MapControllerRoute(
    name: "admin",
    pattern: "adminka",
    defaults: new { controller = "Admin", action = "Index" });

app.Run();

