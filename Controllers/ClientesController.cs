using Microsoft.AspNetCore.Mvc;
using TareaExamenClientes.Models;
using System.Collections.Generic;
using System.Linq;

namespace TareaExamenClientes.Controllers
{
    public class ClientesController : Controller
    {
        // Simulador de base de datos en memoria
        private static List<Cliente> _clientes = new List<Cliente>();
        private static int _nextId = 1;

        // 1. Acción Index (Muestra la lista)
        public IActionResult Index()
        {
            return View(_clientes);
        }

        // 2. Acción Detalle (Busca por ID)
        public IActionResult Detalle(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            
            if (cliente == null)
            {
                // Devuelve 404 si el cliente no existe, como pide la rúbrica
                return NotFound(); 
            }

            return View(cliente);
        }

        // 3. Acción Crear (GET - Muestra el formulario vacío)
        public IActionResult Crear()
        {
            return View();
        }

        // 4. Acción Crear (POST - Recibe los datos del formulario)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Cliente cliente)
        {
            // Verifica si el modelo cumple con las Data Annotations y la validación de edad
            if (ModelState.IsValid)
            {
                cliente.Id = _nextId++;
                _clientes.Add(cliente);

                // Patrón PRG: Redirige a Index después de un POST exitoso
                return RedirectToAction(nameof(Index));
            }

            // Si hay errores (ej. es menor de 18), recarga la vista mostrando los errores
            return View(cliente);
        }
    }
}