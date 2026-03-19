using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TareaExamenClientes.Controllers;

[Authorize(Policy = "SoloAdminUnicda")]
public class UsuariosController : Controller {
    private readonly UserManager<IdentityUser> _userManager;

    public UsuariosController(UserManager<IdentityUser> userManager) {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index() {
        var usuarios = await _userManager.Users.ToListAsync();
        return View(usuarios);
    }
}