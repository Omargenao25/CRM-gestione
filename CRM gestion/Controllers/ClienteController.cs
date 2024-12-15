using CRM_gestion.Cliente_repositorio;
using CRM_gestion.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index() => ExecuteSafe(() => View(_clienteRepository.GetAll()), "No se pudieron cargar los clientes.");

        // Ver detalles de un cliente específico
        public IActionResult Details(int id) =>
            id <= 0 ? BadRequest("El ID proporcionado no es válido.") : ExecuteSafe(() => {
                var cliente = _clienteRepository.GetById(id);
                return cliente == null ? NotFound() : View(cliente);
            });

        // Mostrar formulario para crear un nuevo cliente
        public IActionResult Create() => View();

        // Guardar un nuevo cliente
        [HttpPost]
        public IActionResult Create(Cliente cliente) => ExecuteValidated(cliente, () => {
            _clienteRepository.InsertarCliente(cliente);
            return RedirectToAction("Index");
        });

        // Mostrar formulario para editar un cliente existente
        public IActionResult Edit(int id) =>
            id <= 0 ? BadRequest("El ID proporcionado no es válido.") : ExecuteSafe(() => {
                var cliente = _clienteRepository.GetById(id);
                return cliente == null ? NotFound() : View(cliente);
            });

        // Guardar cambios en un cliente existente
        [HttpPost]
        public IActionResult Edit(Cliente cliente) => ExecuteValidated(cliente, () => {
            if (_clienteRepository.GetById(cliente.Id) == null)
            {
                return NotFound("El cliente no existe.");
            }
            _clienteRepository.ActualizarCliente(cliente);
            return RedirectToAction("Index");
        });

        // Eliminar un cliente
        [HttpPost]
        public IActionResult Delete(int id) =>
            id <= 0 ? BadRequest("El ID proporcionado no es válido.") : ExecuteSafe(() => {
                var cliente = _clienteRepository.GetById(id);
                if (cliente == null)
                {
                    return NotFound("El cliente no existe.");
                }
                _clienteRepository.BorrarCliente(cliente.Id);
                return RedirectToAction("Index");
            });

        // Método para manejo de errores genérico
        private IActionResult ExecuteSafe(Func<IActionResult> action, string errorMessage = "Se produjo un error.")
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                // Opcional: Registrar el error
                ViewBag.ErrorMessage = errorMessage;
                return View("Error");
            }
        }

        // Método para validación genérica de modelo
        private IActionResult ExecuteValidated(Cliente cliente, Func<IActionResult> action)
        {
            if (cliente == null)
            {
                ModelState.AddModelError("", "El cliente no puede ser nulo.");
                return View(cliente);
            }
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }
            return ExecuteSafe(action);
        }
    }
}
