using CRM_gestion.Cliente_repositorio;
using CRM_gestion.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_gestion.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteRepository clienteRepository;

        public ClienteController()
        {
            this.clienteRepository = new ClienteRepository();
        }

        public ViewResult Index()
        {
            var clientes = clienteRepository.GetAll();
            return View(clientes);
        }

        // Método para mostrar la vista de registro
        public IActionResult Create()
        {
            return View();
        }

        // Método para procesar el formulario de registro
        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                clienteRepository.InsertarCliente(cliente);
                TempData["Message"] = "Cliente registrado correctamente.";
                return RedirectToAction("Index");
            }
            return View(cliente);
        }
    }
}