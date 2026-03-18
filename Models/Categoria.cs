using System.ComponentModel.DataAnnotations;
namespace TareaExamenClientes.Models;

public class Categoria {
    public int Id { get; set; }
    [Required, StringLength(80)]
    public string Nombre { get; set; } = "";
    // Relación: Una categoría tiene muchos productos
    public ICollection<Producto> Productos { get; set; } = [];
}