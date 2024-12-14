using CRM_gestion.Cliente_repositorio;
using CRM_gestion.Data;
using CRM_gestion.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_gestion.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteController()
        {
            this._clienteRepository = new ClienteRepository(new CRM_gestionContext());
        }

        // Listar todos los clientes
        public IActionResult Index()
        {
            var clientes = _clienteRepository.GetAll();
            return View(clientes);
        }

        // Mostrar formulario para crear un nuevo cliente
        public IActionResult Create()
        {
            return View();
        }

        // Guardar un nuevo cliente
        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.InsertarCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // Mostrar formulario para editar un cliente existente
        public IActionResult Edit(int id)
        {
            var cliente = _clienteRepository.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // Guardar cambios en un cliente existente
        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepository.ActualizarCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // Eliminar un cliente
        public IActionResult Delete(int id)
        {
            _clienteRepository.BorrarCliente(id);
            return RedirectToAction("Index");
        }

        // Ver detalles de un cliente específico
        public IActionResult Details(int id)
        {
            var cliente = _clienteRepository.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
    }
}
