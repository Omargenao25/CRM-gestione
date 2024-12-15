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
            var Deudas = _context.Deudas
                        .Include(d => d.Cobros)
                        .ToList();

            return View(Deudas);
        }

        public IActionResult Details(int id)
        {
            var Deuda = _context.Deudas
                        .Include(d => d.Cobros)
                        .FirstOrDefault(d => d.Id == id);

            if (Deuda == null)
            {
                return NotFound();
            }
            return View(Deuda);
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
                ModelState.AddModelError("", "Error al cargar la lista de clientes.");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Deuda deuda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Verificar si ClienteId está asignado y cargar el cliente correspondiente
                    deuda.Cliente = _context.Clientes.Find(deuda.ClienteId);

                    _context.Deudas.Add(deuda);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar la deuda: " + ex.Message);
                }
            }

            // Si hay errores, recargar la lista de clientes
            var clientes = _clienteRepository.GetAll();
            ViewData["Clientes"] = new SelectList(clientes, "Id", "Nombre");
            return View(deuda);
        }

        public IActionResult CreateCobros(int id)
        {
            var Deuda = _context.Deudas.Find(id);
            if (Deuda == null)
            {
                return NotFound();
            }
            return View(Deuda);
        }

        [HttpPost]
        public IActionResult CreateCobro(int IdDeuda, Cobro cobro)
        {
            if (!ModelState.IsValid)
            {
                return View(cobro);
            }

            var Deuda = _context.Deudas.Find(IdDeuda);

            if (Deuda == null)
            {
                ModelState.AddModelError("", "La deuda especificada no existe.");
                return View(cobro);
            }

            cobro.DeudaId = Deuda.Id;
            cobro.FechaCobro = DateTime.Now;

            try
            {
                _context.Cobros.Add(cobro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar el cobro.");
                return View(cobro);
            }
        }
    }
}