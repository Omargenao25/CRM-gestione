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
using CRM_gestion.Models.ViewModels;

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
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {

            var totalDeudas = _context.Deudas
                        .Include(d => d.Cliente)
                        .Include(d => d.Cobros)
                        .OrderByDescending(d => d.FechaCreación)
                        .ThenByDescending(d => d.Monto);

            //Paginar Resultados
            var deudas = totalDeudas
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize);

            var totalItems = await totalDeudas.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var viewModel = new DeudasViewModel
            {
                Deudas = await deudas.ToListAsync(),
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize
            };

            return View(viewModel);

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
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(deuda);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(InvalidDataException)
            {
                ModelState.AddModelError(string.Empty, "Error al guardar los datos");
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", deuda.ClienteId);
            return View(deuda);
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
        public async Task<IActionResult> Edit([Bind("DeudaId,Monto,FechaCreación,FechaVencimiento,ClienteId")] Deuda deuda)
        {
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nombre", deuda.ClienteId);
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
        public async Task<IActionResult> CreateCobro(
        int DeudaId,
        [Bind("DeudaId", "CobroId", "Monto", "FechaCobro")] Cobro cobro)
        {
            if (cobro == null)
            {
                ModelState.AddModelError(string.Empty, "El objeto cobro no puede ser nulo.");
                return View(cobro);
            }

            if (!DeudaExists(DeudaId))
            {
                ModelState.AddModelError(string.Empty, "La deuda no existe.");
                return View(cobro);
            }

            var deuda = await _context.Deudas
                                      .Include(d => d.Cobros)
                                      .Include(d => d.Cliente) // Incluye información del cliente para la vista
                                      .FirstOrDefaultAsync(d => d.DeudaId == DeudaId);

            if (deuda == null)
            {
                return NotFound("Deuda no encontrada.");
            }

            // Calcular el total cobrado incluyendo el nuevo cobro
            var totalConNuevoCobro = deuda.TotalCobrado + cobro.Monto;

            if (totalConNuevoCobro > deuda.Monto)
            {
                ModelState.AddModelError(string.Empty,
                    $"El monto total ({totalConNuevoCobro:C}) excede el monto de la deuda ({deuda.Monto:C}).");
                
                return View(new Cobro
                {
                    DeudaId = deuda.DeudaId,
                    Deuda = deuda
                });
            }

            try
            {
                if (ModelState.IsValid)
                {
                    cobro.DeudaId = deuda.DeudaId;
                    _context.Add(cobro);

                    // Actualizar el total cobrado
                    deuda.TotalCobrado = totalConNuevoCobro;
                    _context.Update(deuda);
                    await _context.SaveChangesAsync();

                    // Redirigir a los detalles de la deuda
                    return RedirectToAction(nameof(Details), "Deudas", new { id = DeudaId });
                }
            }
            catch (Exception ex)
            {
                // Log del error
                Console.Error.WriteLine($"Error al guardar el cobro: {ex.Message}");

                ModelState.AddModelError(string.Empty,
                    "No se pudieron guardar los cambios. Intenta nuevamente y contacta al administrador si el problema persiste.");
            }

            // Devolver la vista con datos cargados en caso de error
            return View(new Cobro
            {
                DeudaId = deuda.DeudaId,
                Deuda = deuda
            });
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
            var deuda = await _context.Deudas
                                        .Include(d => d.Cliente)
                                        .Include(d => d.Cobros)
                                        .FirstOrDefaultAsync(m => m.DeudaId == id);

            if (deuda != null)
            {
                if (deuda.Cobros.Count > 0)
                {
                    // Si tiene cobros, agregar el mensaje de error
                    TempData["ErrorMessage"] = "No puedes eliminar una deuda con cobros asociados.";
                    // Retornar la vista con la deuda y el mensaje de error
                    return View(deuda);
                }
                else
                {
                    // Si no tiene cobros, proceder con la eliminación
                    _context.Deudas.Remove(deuda);
                    await _context.SaveChangesAsync();
                    // Redirigir a la acción Index después de eliminar la deuda
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "La deuda no existe.";
                return RedirectToAction(nameof(Index));
            }
        }

        private bool DeudaExists(int id)
        {
            return _context.Deudas.Any(e => e.DeudaId == id);
        }
    }
}
