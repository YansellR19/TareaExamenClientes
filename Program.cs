using Microsoft.EntityFrameworkCore;
using TareaExamenClientes.Data;
using TareaExamenClientes.Models; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar EF Core con SQL Server
builder.Services.AddDbContext<ClienteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// LÓGICA DE DATOS DE PRUEBA (SEED DATA)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ClienteContext>();
    context.Database.EnsureCreated(); // Crea la BD si no existe

    // Solo agrega datos si la tabla Categorias está vacía
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
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/Home/ErrorStatus", "?statusCode={0}");
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();