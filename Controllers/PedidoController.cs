using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareaExamenClientes.Data;
using TareaExamenClientes.Models;

namespace TareaExamenClientes.Controllers;

public class PedidoController : Controller {
    private readonly ClienteContext _ctx;
    public PedidoController(ClienteContext ctx) => _ctx = ctx;

    public async Task<IActionResult> Index() {
        return View(await _ctx.Pedidos.ToListAsync());
    }

    // Detalle con navegación anidada
    public async Task<IActionResult> Detalle(int id) {
        var pedido = await _ctx.Pedidos
            .Include(p => p.PedidoProductos)
                .ThenInclude(pp => pp.Producto)
            .FirstOrDefaultAsync(p => p.Id == id);

        return pedido == null ? NotFound() : View(pedido);
    }
    // GET: Muestra el formulario y la lista de productos disponibles
public async Task<IActionResult> Crear()
{
    // Traemos los productos para que el usuario elija
    ViewBag.Productos = await _ctx.Productos.ToListAsync();
    return View();
}

// POST: Guarda el pedido y sus productos asociados
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Crear(string ClienteNombre, int[] productosSeleccionados)
{
    if (string.IsNullOrEmpty(ClienteNombre) || productosSeleccionados == null || productosSeleccionados.Length == 0)
    {
        ModelState.AddModelError("", "Debe ingresar un cliente y seleccionar al menos un producto.");
        ViewBag.Productos = await _ctx.Productos.ToListAsync();
        return View();
    }

    // 1. Creamos el objeto Pedido
    var nuevoPedido = new Pedido
    {
        ClienteNombre = ClienteNombre,
        Fecha = DateTime.Now
    };

    // 2. Agregamos los productos seleccionados a la tabla de unión
    foreach (var productoId in productosSeleccionados)
    {
        nuevoPedido.PedidoProductos.Add(new PedidoProducto
        {
            ProductoId = productoId,
            Cantidad = 1 // Por simplicidad, asignamos 1 unidad
        });
    }

    // 3. Guardamos todo en una sola transacción
    _ctx.Pedidos.Add(nuevoPedido);
    await _ctx.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}
}