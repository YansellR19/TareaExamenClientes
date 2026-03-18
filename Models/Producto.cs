using System.ComponentModel.DataAnnotations;
namespace TareaExamenClientes.Models;

public class Producto {
    public int Id { get; set; }
    [Required, StringLength(100)]
    public string Nombre { get; set; } = "";
    [Required, Range(0.01, 999999)]
    public decimal Precio { get; set; }
    
    // Clave Foránea (FK)
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; } // Propiedad de navegación
}