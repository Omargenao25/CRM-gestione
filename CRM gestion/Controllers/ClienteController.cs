using CRM_gestion.Cliente_repositorio;
using CRM_gestion.Data;
using CRM_gestion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CRM_gestion.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteController(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        }

        // Listar todos los clientes
        public IActionResult Index()
        {
            try
            {
                var clientes = _clienteRepository.GetAll();
                return View(clientes);
            }
            catch (Exception ex)
            {
                // Opcional: Registrar el error
                ViewBag.ErrorMessage = "No se pudieron cargar los clientes.";
                return View("Error");
            }
        }

        // Ver detalles de un cliente específico
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID proporcionado no es válido.");
            }

            var cliente = _clienteRepository.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
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
            if (cliente == null)
            {
                ModelState.AddModelError("", "El cliente no puede ser nulo.");
                return View(cliente);
            }

            if (!ModelState.IsValid)
            {
                return View(cliente); // Asegúrate de pasar el cliente con los datos introducidos
            }

            try
            {
                _clienteRepository.InsertarCliente(cliente);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Opcional: Registrar el error
                ModelState.AddModelError("", "Ocurrió un error al guardar el cliente.");
                return View(cliente); // Devuelve el modelo con los datos introducidos
            }
        }

        // Mostrar formulario para editar un cliente existente
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID proporcionado no es válido.");
            }

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
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            try
            {
                var clienteExistente = _clienteRepository.GetById(cliente.Id);
                if (clienteExistente == null)
                {
                    return NotFound("El cliente no existe.");
                }

                _clienteRepository.ActualizarCliente(cliente);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Opcional: Registrar el error
                ModelState.AddModelError("", "Ocurrió un error al actualizar el cliente.");
                return View(cliente);
            }
        }

        [HttpPost]
        // Eliminar un cliente
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID proporcionado no es válido.");
            }

            try
            {
                var cliente = _clienteRepository.GetById(id);
                if (cliente == null)
                {
                    return NotFound("El cliente no existe.");
                }

                _clienteRepository.BorrarCliente(cliente.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Opcional: Registrar el error
                TempData["ErrorMessage"] = "Ocurrió un error al eliminar el cliente.";
                return RedirectToAction("Index");
            }
        }
    }
}
