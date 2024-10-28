using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using WebBanHang.Models;
using WebBanHang.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//đăng ký DbContext
builder.Services.AddDbContext<WebDbContext>((options) =>
{
    string connectString = builder.Configuration.GetConnectionString("AppConnectString");
    options.UseSqlServer(connectString);
});


//đăng ký session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session tồn tại 30 phút
    options.Cookie.HttpOnly = true; // Cookie chỉ truy cập qua HTTP, không qua JavaScript
    options.Cookie.IsEssential = true; // Cookie này là thiết yếu, luôn được lưu bất kể quyền riêng tư
});


builder.Services.AddHttpContextAccessor(); // Đăng ký IHttpContextAccessor để hỗ trợ lấy base url

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


app.UseSession();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
