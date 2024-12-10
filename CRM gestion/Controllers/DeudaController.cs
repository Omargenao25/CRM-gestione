using CRM_gestion.Cliente_repositorio;
using CRM_gestion.Data;
using CRM_gestion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM_gestion.Controllers
{
    public class DeudaController : Controller
    {
        private List<Deuda> deudas;
        private ClienteRepository clienteRepository;

        public DeudaController()
        {
            this.deudas = new List<Deuda>();
            this.clienteRepository = new ClienteRepository();

        }

        public ViewResult Index()
        {
            return View(deudas);
        }

        public ViewResult Create()
        {
            var clientes = clienteRepository.GetAll();
            ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Deuda deuda)
        {
            if (ModelState.IsValid)
            {
                // Cargar el cliente desde el repositorio
                var cliente = clienteRepository.GetById(deuda.ClienteId);
                if (cliente == null)
                {
                    ModelState.AddModelError("ClienteId", "El cliente seleccionado no existe.");
                    var Grupoclientes = clienteRepository.GetAll();
                    ViewBag.clientes = new SelectList(Grupoclientes, "Id", "Nombre");
                    return View(deuda);
                }

                deuda.Cliente = cliente; // Asignar la propiedad de navegación
                deuda.Id = deudas.Any() ? deudas.Max(c => c.Id) + 1 : 1;
                deudas.Add(deuda);

                return RedirectToAction("Index");
            }

            // Si el modelo no es válido, recarga los datos necesarios
            var clientes = clienteRepository.GetAll();
            ViewBag.clientes = new SelectList(clientes, "Id", "Nombre");
            return View(deuda);
        }
    }
}