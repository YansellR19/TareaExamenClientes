using Microsoft.EntityFrameworkCore;
using TareaExamenClientes.Models;

namespace TareaExamenClientes.Data;

public class ClienteContext : DbContext {
    public ClienteContext(DbContextOptions<ClienteContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Categoria> Categorias { get; set; } 
    public DbSet<Producto> Productos { get; set; }  
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoProducto> PedidoProductos { get; set; }

    protected override void OnModelCreating(ModelBuilder mb) {
        // 1. Configurar clave compuesta para la relación N:N
        mb.Entity<PedidoProducto>()
            .HasKey(pp => new { pp.PedidoId, pp.ProductoId });

        // 2. Configurar precisión para el precio decimal
        mb.Entity<Producto>().Property(p => p.Precio).HasPrecision(18, 2);
    }
}