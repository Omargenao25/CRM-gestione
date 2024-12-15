using CRM_gestion.Cliente_repositorio;
using CRM_gestion.Data;
using CRM_gestion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRM_gestion.Controllers
{
    public class DeudaController : Controller
    {
        private readonly CRM_gestionContext _context;
        private readonly ClienteRepository _clienteRepository;

        public DeudaController(CRM_gestionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _clienteRepository = new ClienteRepository(_context);
        }

        // Listar todas las deudas
        public IActionResult Index() => ExecuteSafe(() =>
        {
            var deudas = _context.Deudas
                        .Include(d => d.Cobros)
                        .Include(d => d.Cliente)
                        .ToList();
            return View(deudas);
        }, "No se pudieron cargar las deudas.");

        // Ver detalles de una deuda específica
        public IActionResult Details(int id) =>
            id <= 0 ? BadRequest("El ID proporcionado no es válido.") : ExecuteSafe(() =>
            {
                var deuda = _context.Deudas
                            .Include(d => d.Cobros)
                            .Include(d => d.Cliente)
                            .FirstOrDefault(d => d.Id == id);
                return deuda == null ? NotFound() : View(deuda);
            });

        // Mostrar formulario para crear una nueva deuda
        public IActionResult Create() => ExecuteSafe(() =>
        {
            var clientes = _clienteRepository.GetAll();
            ViewData["Clientes"] = new SelectList(clientes, "Id", "Nombre");
            return View();
        }, "Error al cargar la lista de clientes.");

        // Guardar una nueva deuda
        [HttpPost]
        public IActionResult Create(Deuda deuda) => ExecuteValidated(deuda, () =>
        {
            _context.Deudas.Add(deuda);
            _context.SaveChanges();
            return RedirectToAction("Index");
        });

        // Mostrar formulario para crear cobros de una deuda específica
        public IActionResult CreateCobros(int id) => ExecuteSafe(() =>
        {
            var deuda = _context.Deudas
                        .Include(d => d.Cliente)
                        .FirstOrDefault(d => d.Id == id);
            return deuda == null ? NotFound() : View(deuda);
        });

        // Guardar un nuevo cobro para una deuda
        [HttpPost]
        public IActionResult CreateCobro(int idDeuda, Cobro cobro) => ExecuteValidated(cobro, () =>
        {
            var deuda = _context.Deudas
                        .Include(d => d.Cliente)
                        .FirstOrDefault(d => d.Id == idDeuda);

            if (deuda == null)
            {
                ModelState.AddModelError("", "La deuda especificada no existe.");
                return View(cobro);
            }

            cobro.DeudaId = deuda.Id;
            cobro.FechaCobro = DateTime.Now;

            _context.Cobros.Add(cobro);
            _context.SaveChanges();
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
        private IActionResult ExecuteValidated(object model, Func<IActionResult> action)
        {
            if (model == null)
            {
                ModelState.AddModelError("", "El modelo no puede ser nulo.");
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return ExecuteSafe(action);
        }
    }
}