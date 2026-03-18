using System.ComponentModel.DataAnnotations;
namespace TareaExamenClientes.Models;

public class Pedido {
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    [Required]
    public string ClienteNombre { get; set; } = "";
    // Relación N:N a través de la tabla de unión
    public ICollection<PedidoProducto> PedidoProductos { get; set; } = [];
}