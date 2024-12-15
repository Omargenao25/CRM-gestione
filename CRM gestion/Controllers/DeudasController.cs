using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM_gestion.Data;
using CRM_gestion.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CRM_gestion.Controllers
{
    public class DeudasController : Controller
    {
        private readonly CRM_gestionContext _context;

        public DeudasController(CRM_gestionContext context)
        {
            _context = context;
        }

        // GET: Deudas
        public async Task<IActionResult> Index()
        {
            var Deudas = _context.Deudas
                        .Include(d => d.Cliente)
                        .Include(d => d.Cobros);

            return View(await Deudas.ToListAsync());
        }

        // GET: Deudas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deuda = await _context.Deudas
                              .Include(d => d.Cliente)
                              .Include(d => d.Cobros)
                              .FirstOrDefaultAsync(m => m.DeudaId == id);

            if (deuda == null)
            {
                return NotFound();
            }

            return View(deuda);
        }

        // GET: Deudas/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre");
            return View();
        }

        // POST: Deudas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeudaId,Monto,FechaCreación,FechaVencimiento,ClienteId")] Deuda deuda)
        {
            _context.Add(deuda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Deudas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deuda = await _context.Deudas.FindAsync(id);

            if (deuda == null)
            {
                return NotFound();
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", deuda.ClienteId);
            return View(deuda);
        }

        // POST: Deudas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeudaId,Monto,FechaCreación,FechaVencimiento,ClienteId")] Deuda deuda)
        {
            if (id != deuda.DeudaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deuda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeudaExists(deuda.DeudaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "ClienteId", deuda.ClienteId);
            return View(deuda);
        }

        // GET: Deudas/Cobros/Create
        public async Task<IActionResult> CreateCobro(int? id)
        {
            if (id == null)
            {
                return NotFound("ID de la deuda no proporcionado");
            }

            // Carga ansiosa de la deuda, cliente y cobros asociados
            var deuda = await _context.Deudas
                .Include(d => d.Cliente) // Cargar datos del cliente
                .Include(d => d.Cobros) // Cargar cobros asociados
                .FirstOrDefaultAsync(d => d.DeudaId == id);

            if (deuda == null)
            {
                return NotFound($"No se encontró una deuda con ID {id}");
            }

            // Crear un nuevo objeto Cobro asociado a esta deuda
            var nuevoCobro = new Cobro
            {
                DeudaId = deuda.DeudaId,
                Deuda = deuda // Asociar la deuda completa al modelo de cobro
            };

            return View(nuevoCobro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCobro(int DeudaId, [Bind("DeudaId","CobroId","Monto","FechaCobro")] Cobro cobro)
        {
            var deuda = await _context.Deudas
                                .Include(d => d.Cobros)
                                .FirstOrDefaultAsync(d => d.DeudaId == DeudaId);

            if (deuda == null)
            {
                return NotFound("Deuda no encontrada");
            }

            var TotalCobrado = await _context
                                    .Cobros.Where(c => c.DeudaId == DeudaId)
                                    .SumAsync(c => c.Monto);

            if (TotalCobrado + cobro.Monto > deuda.Monto)
            {
                return BadRequest($"El monto total ({TotalCobrado + cobro.Monto}) excede el monto de la deuda ({deuda.Monto}).");
            }

            cobro.DeudaId = deuda.DeudaId;
            _context.Add(cobro);
            await _context.SaveChangesAsync();

            //Redirigir a los detalles de la deuda
            return RedirectToAction("Details","Deudas", new {id = DeudaId});
        }

        // GET: Deudas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deuda = await _context.Deudas
                                          .Include(d => d.Cliente)
                                          .Include(d => d.Cobros)
                                          .FirstOrDefaultAsync(m => m.DeudaId == id);

            if (deuda == null)
            {
                return NotFound();
            }

            return View(deuda);
        }

        // POST: Deudas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deuda = await _context.Deudas.FindAsync(id);
            if (deuda != null)
            {
                _context.Deudas.Remove(deuda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeudaExists(int id)
        {
            return _context.Deudas.Any(e => e.DeudaId == id);
        }
    }
}
