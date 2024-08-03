using FastTypingApp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
//������ WebApplicationBuilder � �������������� ������������ ���������� �� ��������� � �������������� CreateBuilder() ������
//(����� ����������� ���������� ���-������ � ���������� �������� ������� ����������� � ���� �������� ���������� ��� ������ appsettings.json)
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllersWithViews(); //����� ����������, ������� ������������ ����, ����������� ��� ���������� MVC (������, �������������, ����������) ��� ��������� ������������.
                                            //�� �������� � ���� ��� ����������� ������ � ������������ ��� MVC, ����� ���� ���������� ����� ������������ ����������� MVC.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; //���������� ���� ��������������� ��� �� �������������������� ������������
        options.AccessDeniedPath = "/accessdenied"; //��� �������������������� ������������, ������� �� ����� ���� ��� ������� � �������
    });

builder.Services.AddAuthorization();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

//����������� ������������� ����������� ����������� ������������� � ����������� 
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


//���������� ������ �������� �� ���������, ������� ����������, ����� ����������, �������� � �������������� ��������� �������� ������� ������������ ��� ��������� �������� ��������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//��������� ����������
app.Run();
