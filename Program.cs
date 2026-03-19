using Microsoft.AspNetCore.Identity; //
using Microsoft.EntityFrameworkCore;
using TareaExamenClientes.Data;
using TareaExamenClientes.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. SERVICIOS BÁSICOS
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); 

// 2. CONFIGURACIÓN DE BASE DE DATOS
builder.Services.AddDbContext<ClienteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 3. CONFIGURACIÓN DE IDENTITY Y ROLES
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>() // Habilita el manejo de roles
.AddEntityFrameworkStores<ClienteContext>();

// 4. POLÍTICAS DE AUTORIZACIÓN (Puntos extra del examen)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SoloAdminUnicda", policy => 
        policy.RequireRole("Admin")
              .RequireClaim(System.Security.Claims.ClaimTypes.Email, "admin@unicda.edu.do"));
});

// 5. CONFIGURACIÓN DE RUTAS DE ACCESO
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirección si no tiene permiso
});

var app = builder.Build();

// 6. LÓGICA DE DATOS DE PRUEBA (SEED DATA)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ClienteContext>();
    
    context.Database.EnsureCreated(); // Asegura que la BD existe

    // Seed de Categorías y Productos
    if (!context.Categorias.Any())
    {
        var electronica = new Categoria { Nombre = "Electrónica" };
        var hogar = new Categoria { Nombre = "Hogar" };
        context.Categorias.AddRange(electronica, hogar);

        context.Productos.AddRange(
            new Producto { Nombre = "Laptop Dell", Precio = 950.00m, Categoria = electronica },
            new Producto { Nombre = "Mouse Gamer", Precio = 45.00m, Categoria = electronica },
            new Producto { Nombre = "Cafetera Pro", Precio = 120.00m, Categoria = hogar }
        );
        context.SaveChanges();
    }

    // Seed de Roles y Usuario Admin (Ejecución asíncrona)
    await DbInitializer.SeedRolesAndAdminAsync(services);
}

// 7. CONFIGURACIÓN DEL PIPELINE HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/Home/ErrorStatus", "?statusCode={0}");
app.UseStaticFiles();
app.UseRouting();


app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Mapea las páginas de Login/Registro de Identity

app.Run();