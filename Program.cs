using HackaTec.Hubs;
using HackaTec.Models.Entities;
using HackaTec.Repositories;
using HackaTec.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Account/Login";
        options.AccessDeniedPath = "/Admin/Account/Login";
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<HackatecContext>();

builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<FeedService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<DonantesService>();
builder.Services.AddSignalR();


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapDefaultControllerRoute();

app.MapHub<ChatHub>("/chathub");

app.Run();
