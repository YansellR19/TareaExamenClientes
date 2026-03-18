using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TareaExamenClientes.Data;
using TareaExamenClientes.Models;

namespace TareaExamenClientes.Controllers;

public class ProductoController : Controller
{
    private readonly ClienteContext _ctx;

    public ProductoController(ClienteContext ctx) => _ctx = ctx;

    // READ: Usamos Include para unir las tablas
    public async Task<IActionResult> Index()
    {
        var productos = await _ctx.Productos
            .Include(p => p.Categoria) 
            .ToListAsync();
        return View(productos);
    }

    // CREATE (GET): Llenamos el dropdown de categorías
    public async Task<IActionResult> Crear()
    {
        ViewBag.Categorias = new SelectList(await _ctx.Categorias.ToListAsync(), "Id", "Nombre");
        return View();
    }

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