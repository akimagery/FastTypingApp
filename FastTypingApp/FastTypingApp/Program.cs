using FastTypingApp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
//объект WebApplicationBuilder с предварительно настроенными значениями по умолчанию с использованием CreateBuilder() метода
//(метод настраивает внутренний веб-сервер и определяет корневой каталог содержимого и файл настроек приложения для чтения appsettings.json)
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllersWithViews(); //метод расширения, который регистрирует типы, необходимые для приложения MVC (модель, представление, контроллер) для внедрения зависимостей.
                                            //Он включает в себя все необходимые службы и конфигурации для MVC, чтобы ваше приложение могло использовать архитектуру MVC.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; //определяет путь перенаправления для не аутентифицированного пользователя
        options.AccessDeniedPath = "/accessdenied"; //для аутентифицированного пользователя, который не имеет прав для доступа к ресурсу
    });

builder.Services.AddAuthorization();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

//настраивает промежуточное программное обеспечение маршрутизации и авторизации 
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


//определяет шаблон маршрута по умолчанию, который определяет, какой контроллер, действие и необязательные параметры маршрута следует использовать для обработки входящих запросов
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//запускает приложение
app.Run();
