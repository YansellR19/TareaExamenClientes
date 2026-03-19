using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareaExamenClientes.Data;
using TareaExamenClientes.Models;
using Microsoft.AspNetCore.Authorization;

namespace TareaExamenClientes.Controllers;

[Authorize(Roles = "Admin")]
public class CategoriaController : Controller
{
    private readonly ClienteContext _ctx;

    public CategoriaController(ClienteContext ctx) => _ctx = ctx;

    // Listar todas las categorías
    public async Task<IActionResult> Index()
    {
        return View(await _ctx.Categorias.OrderBy(c => c.Nombre).ToListAsync());
    }

    public IActionResult Crear() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            _ctx.Add(categoria);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(categoria);
    }
}