using MudBlazor.Services;
using Proyecto.Components;
using BD.Data;
using Microsoft.EntityFrameworkCore;
using Bycript;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);




// Add MudBlazor services
builder.Services.AddControllers();  // Registra controllers en DI
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

dotenv.net.DotEnv.Load();
string db = Environment.GetEnvironmentVariable("db");

builder.Services.AddDbContext<ProyectoContext>
(
opt =>
{
    opt.UseMySQL(db);
}

);


// Configurar autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.Cookie.MaxAge = TimeSpan.FromDays(7);
         options.AccessDeniedPath = "/AccessDenied";
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("Administrador"))
    .AddPolicy("UserOnly", policy => policy.RequireRole("Usuario", "Administrador"));

// Registrar servicios
builder.Services.AddScoped<IBCryptService, BCryptService>();
builder.Services.AddScoped<Proyecto.Mapper.Map>();
builder.Services.AddScoped<Proyecto.Services.IUsuarioService, Proyecto.Services.UsuarioService>();
builder.Services.AddScoped<Proyecto.Services.IEmpleadoService, Proyecto.Services.EmpleadoService>();
builder.Services.AddScoped<Proyecto.Services.IAreaService, Proyecto.Services.AreaService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();



app.MapPost("/api/auth/login", async (HttpContext context, Proyecto.Services.IUsuarioService usuarioService) =>
{
    var username = context.Request.Form["username"].ToString();
    var password = context.Request.Form["password"].ToString();
    
    var user = await usuarioService.AuthenticateAsync(username, password);

    if (user != null)
    {
       
        
         var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Usuario1),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role,user.IsAdmin?"Administrador":"Usuario")
        };

        if (user.Empleado != null)
        {
            claims.Add(new System.Security.Claims.Claim("EmpleadoId", user.Empleado.Id.ToString()));
            claims.Add(new System.Security.Claims.Claim("EmpleadoNombre", $"{user.Empleado.PrimerNombre} {user.Empleado.PrimerApellido}"));
        }

        var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new System.Security.Claims.ClaimsPrincipal(identity);
        
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        
        context.Response.Redirect("/");
       
    }
    else
    {
        context.Response.Redirect("/login?error=true");
    }
});

app.MapPost("/api/auth/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});



app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Ejecutar datos semilla
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProyectoContext>();
    var bcryptService = scope.ServiceProvider.GetRequiredService<IBCryptService>();
    
    // Asegurar que la base de datos esté creada
    context.Database.EnsureCreated();
    
    // Ejecutar datos semilla
    await Proyecto.Data.SeedData.SeedUsuarios(context, bcryptService);
}

app.Run();
