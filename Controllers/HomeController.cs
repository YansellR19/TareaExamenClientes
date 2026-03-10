using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TareaExamenClientes.Models;

namespace TareaExamenClientes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // 👇 AQUÍ ESTÁ EL NUEVO CÓDIGO PARA MANEJAR EL 404 👇
    [Route("/Home/ErrorStatus")]
    public IActionResult ErrorStatus(int statusCode)
    {
        if (statusCode == 404)
        {
            return View("NotFound"); // Llama a la vista NotFound.cshtml que creamos
        }
        return View("Error"); // Llama a la vista de error 500 por defecto
    }
}