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
        // No establezcas LoginPath aquí, usa eventos personalizados
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                var request = context.HttpContext.Request;
                var path = request.Path.ToString();
                var returnUrl = context.RedirectUri;

                // Determinar a qué login redirigir según la URL solicitada
                if (path.Contains("/Admin") || returnUrl.Contains("/Admin"))
                {
                    context.RedirectUri = "/Admin/Account/Login";
                }
                else if (path.Contains("/Institucion") || returnUrl.Contains("/Institucion"))
                {
                    context.RedirectUri = "/Institucion/Account/Login";
                }
                else if (path.Contains("/Donante") || returnUrl.Contains("/Donante"))
                {
                    context.RedirectUri = "/Donante/Account/Login";
                }
                else
                {
                    // Si no se detecta área, ir a Admin por defecto
                    context.RedirectUri = "/Admin/Account/Login";
                }

                context.Response.Redirect(context.RedirectUri);
                return Task.CompletedTask;
            },

            OnRedirectToAccessDenied = context =>
            {
                var request = context.HttpContext.Request;
                var path = request.Path.ToString();

                if (path.Contains("/Admin"))
                {
                    context.RedirectUri = "/Admin/Account/AccessDenied";
                }
                else if (path.Contains("/Institucion"))
                {
                    context.RedirectUri = "/Institucion/Account/AccessDenied";
                }
                else if (path.Contains("/Donante"))
                {
                    context.RedirectUri = "/Donante/Account/AccessDenied";
                }
                else
                {
                    context.RedirectUri = "/Account/AccessDenied";
                }

                context.Response.Redirect(context.RedirectUri);
                return Task.CompletedTask;
            }
        };

        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<HackatecContext>();

builder.Services.AddScoped(typeof(Repository<>), typeof(Repository<>));
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<FeedService>();
builder.Services.AddScoped<InstitucionService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<DonantesService>();


builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

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
app.MapHub<ChatHub>("/chatHub");

app.Run();