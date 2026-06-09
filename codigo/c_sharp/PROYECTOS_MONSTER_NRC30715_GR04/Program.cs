using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Data;
using PROYECTOS_MONSTER_NRC30715_GR04.Services;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";

        options.ExpireTimeSpan = TimeSpan.FromHours(8);

        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "USR",
        policy => policy.RequireClaim(
            "OPCION",
            "USR"));

    options.AddPolicy(
        "EMP",
        policy => policy.RequireClaim(
            "OPCION",
            "EMP"));

    options.AddPolicy(
        "PRO",
        policy => policy.RequireClaim(
            "OPCION",
            "PRO"));

    options.AddPolicy(
        "REP",
        policy => policy.RequireClaim(
            "OPCION",
            "REP"));

    options.AddPolicy(
        "PER",
        policy => policy.RequireClaim(
            "OPCION",
            "PER"));

    options.AddPolicy(
    "EMP",
    policy =>
        policy.RequireClaim(
            "OPCION",
            "EMP"));
});

builder.Services.AddDbContext<ProyectoDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IPerfilService,PerfilService>();

builder.Services.AddScoped<IEmpleadoService,EmpleadoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();


