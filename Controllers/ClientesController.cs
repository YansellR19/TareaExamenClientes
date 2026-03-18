using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareaExamenClientes.Data;
using TareaExamenClientes.Models;

namespace TareaExamenClientes.Controllers;

public class ClientesController : Controller
{
    private readonly ClienteContext _context;

    public ClientesController(ClienteContext context)
    {
        _context = context;
    }

    // LISTAR
    public async Task<IActionResult> Index()
    {
        return View(await _context.Clientes.ToListAsync());
    }

    // DETALLE
    public async Task<IActionResult> Detalle(int id)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        return cliente == null ? NotFound() : View(cliente);
    }

    // CREAR (GET)
    public IActionResult Crear() => View();

    // CREAR (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
    }

    // EDITAR (GET)
    public async Task<IActionResult> Editar(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        return cliente == null ? NotFound() : View(cliente);
    }

    // EDITAR (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, Cliente cliente)
    {
        if (id != cliente.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Clientes.Any(e => e.Id == cliente.Id)) return NotFound();
                throw;
            }
        }
        return View(cliente);
    }

    // ELIMINAR (GET - Confirmación)
    public async Task<IActionResult> Eliminar(int id)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(m => m.Id == id);
        return cliente == null ? NotFound() : View(cliente);
    }

    // ELIMINAR (POST - Acción real)
    [HttpPost, ActionName("Eliminar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarConfirmado(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}