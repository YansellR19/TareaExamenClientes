using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TareaExamenClientes.Data;
using TareaExamenClientes.Models;
using Microsoft.AspNetCore.Authorization; //

namespace TareaExamenClientes.Controllers;

// Agregamos la restricción de Rol Admin para todo el controlador
[Authorize(Roles = "Admin")] 
public class ProductoController : Controller
{
    private readonly ClienteContext _ctx;

    public ProductoController(ClienteContext ctx) => _ctx = ctx;

    // READ: Lista de productos con su categoría
    public async Task<IActionResult> Index()
    {
        var productos = await _ctx.Productos
            .Include(p => p.Categoria) 
            .ToListAsync();
        return View(productos);
    }

    // CREATE (GET): Formulario para nuevo producto
    public async Task<IActionResult> Crear()
    {
        ViewBag.Categorias = new SelectList(await _ctx.Categorias.ToListAsync(), "Id", "Nombre");
        return View();
    }

    // CREATE (POST): Guardar en la base de datos
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Producto producto)
    {
        if (ModelState.IsValid)
        {
            _ctx.Add(producto);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categorias = new SelectList(await _ctx.Categorias.ToListAsync(), "Id", "Nombre", producto.CategoriaId);
        return View(producto);
    }
}