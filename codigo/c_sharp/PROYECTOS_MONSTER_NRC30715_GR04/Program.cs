using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PROYECTOS_MONSTER_NRC30715_GR04.Data;
using PROYECTOS_MONSTER_NRC30715_GR04.Services;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Reportes;

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
        policy => policy.RequireAssertion(ctx =>
            ctx.User.HasClaim(c => c.Type == "OPCION" && (c.Value.StartsWith("USR_") || c.Value.StartsWith("PER_") || c.Value == "USR" || c.Value == "PER"))));

    options.AddPolicy(
        "EMP",
        policy => policy.RequireAssertion(ctx =>
            ctx.User.HasClaim(c => c.Type == "OPCION" && (c.Value.StartsWith("DEP_") || c.Value.StartsWith("CAR_") || c.Value.StartsWith("EMP_") || c.Value.StartsWith("SEX_") || c.Value.StartsWith("ECI_") || c.Value == "EMP"))));

    options.AddPolicy(
        "PRO",
        policy => policy.RequireAssertion(ctx =>
            ctx.User.HasClaim(c => c.Type == "OPCION" && (c.Value.StartsWith("PRO_") || c.Value == "PRO"))));

    options.AddPolicy(
        "REP",
        policy => policy.RequireAssertion(ctx =>
            ctx.User.HasClaim(c => c.Type == "OPCION" && (c.Value.StartsWith("REP_") || c.Value == "REP"))));

    options.AddPolicy(
        "PER",
        policy => policy.RequireAssertion(ctx =>
            ctx.User.HasClaim(c => c.Type == "OPCION" && (c.Value.StartsWith("PER_") || c.Value == "PER"))));
});

builder.Services.AddDbContext<ProyectoDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPerfilService, PerfilService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<ISexoService, SexoService>();
builder.Services.AddScoped<IEstadoCivilService, EstadoCivilService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<ICargoService, CargoService>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces.IEmailService, PROYECTOS_MONSTER_NRC30715_GR04.Services.EmailService>();

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


