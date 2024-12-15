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

        public ViewResult Index()
        {
            var deudas = _context.Deudas
                                .Include(d => d.Cobros)
                                .ToList();  // No need to include 'Deuda' again

            return View(deudas);
        }

        public IActionResult Details(int id)
        {
            var deuda = _context.Deudas
                                .Include(d => d.Cobros)  // Include Cobros, no need to include Deuda
                                .FirstOrDefault(d => d.Id == id);  // Find the specific Deuda by ID

            if (deuda == null)
            {
                return NotFound();
            }

            return View(deuda);
        }

        public ViewResult Create()
        {
            try
            {
                var clientes = _clienteRepository.GetAll();
                ViewData["Clientes"] = new SelectList(clientes, "Id", "Nombre");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al cargar la lista de clientes: " + ex.Message);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Deuda deuda)
        {
            try
            {
                // Asignar el cliente a la propiedad Cliente de la deuda
                var cliente = _clienteRepository.GetById(deuda.ClienteId); // Asumiendo que deuda tiene un ClienteId
                deuda.Cliente = cliente; // Asignamos el cliente a la propiedad Cliente

                _context.Deudas.Add(deuda);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar la deuda: " + ex.Message);
                // Recargar la lista de clientes en caso de error
                var clientes = _clienteRepository.GetAll();
                ViewData["Clientes"] = new SelectList(clientes, "Id", "Nombre");
                return View(deuda);
            }
        }

        public IActionResult CreateCobros(int id)
        {
            var deuda = _context.Deudas
                                .Include(d => d.Cliente)  // Cargar la relación con Cliente
                                .Include(d => d.Cobros)   // Cargar la relación con Cobros
                                .FirstOrDefault(d => d.Id == id);

            if (deuda == null)
            {
                return NotFound();
            }

            // Crear un nuevo Cobro y asignar la Deuda
            var cobro = new Cobro { DeudaId = deuda.Id };

            return View(cobro);  // Pasamos un Cobro vacío a la vista
        }

        [HttpPost]
        public IActionResult CreateCobro(int idDeuda, Cobro cobro)
        {
            if (!ModelState.IsValid)
            {
                return View(cobro);
            }

            var deuda = _context.Deudas
                                .Include(d => d.Cobros)  // Cargar la relación con Cobros si es necesario
                                .FirstOrDefault(d => d.Id == idDeuda);

            if (deuda == null)
            {
                ModelState.AddModelError("", "La deuda especificada no existe.");
                return View(cobro);
            }

            cobro.DeudaId = deuda.Id;
            cobro.FechaCobro = DateTime.Now;
            cobro.Deuda = deuda;
            deuda.Cobros.Add(cobro);

            try
            {
                _context.Cobros.Add(cobro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar el cobro: " + ex.Message);
                return View(cobro);
            }
        }
    }
}