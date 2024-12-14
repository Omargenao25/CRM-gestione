using CRM_gestion.Cliente_repositorio;
using CRM_gestion.Data;
using CRM_gestion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM_gestion.Controllers
{
    public class DeudaController : Controller
    {
        private static CRM_gestionContext _context;

        private ClienteRepository clienteRepository;

        public DeudaController(CRM_gestionContext _context)
        {
            _context = _context;
            this.clienteRepository = new ClienteRepository(_context);

        }

        public ViewResult Index()
        {
            var Deudas = _context.Deudas.ToList();
            return View(Deudas);
        }

        public ViewResult Create()
        {
            var clientes = clienteRepository.GetAll();
            ViewBag.Clientes = new SelectList(clientes, "Id", "Nombre");
            return View();
        }
        
        //[HttpPost]
        //public IActionResult Create(Deuda deuda)
        //{
           
        //}
    }
}