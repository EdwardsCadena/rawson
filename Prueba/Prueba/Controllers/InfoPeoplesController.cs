using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba.Models;

namespace Prueba.Controllers
{
    public class InfoPeoplesController : Controller
    {
        private readonly PruebaDbContext _context;

        public InfoPeoplesController(PruebaDbContext context)
        {
            _context = context;
        }

        // GET: InfoPeoples
        public async Task<IActionResult> Index()
        {
              return _context.infoPeoples != null ? 
                          View(await _context.infoPeoples.ToListAsync()) :
                          Problem("Entity set 'PruebaDbContext.infoPeoples'  is null.");
        }

        // GET: InfoPeoples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.infoPeoples == null)
            {
                return NotFound();
            }

            var infoPeople = await _context.infoPeoples
                .FirstOrDefaultAsync(m => m.Id == id);
            if (infoPeople == null)
            {
                return NotFound();
            }

            return View(infoPeople);
        }

        // GET: InfoPeoples/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InfoPeoples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Telefono,Email,Direcion")] InfoPeople infoPeople)
        {
            if (ModelState.IsValid)
            {
                var lstinfo = _context.infoPeoples.Where(x => x.people == infoPeople.people).ToList();
                if (lstinfo.Count == 2)
                {
                    TempData["alert"] = "Solo se puede registrar dos veces la informacion";
                    return View(infoPeople);
                }
                else { 
                _context.Add(infoPeople);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            }
            return View(infoPeople);
        }

        // GET: InfoPeoples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.infoPeoples == null)
            {
                return NotFound();
            }

            var infoPeople = await _context.infoPeoples.FindAsync(id);
            if (infoPeople == null)
            {
                return NotFound();
            }
            return View(infoPeople);
        }

        // POST: InfoPeoples/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Telefono,Email,Direcion")] InfoPeople infoPeople)
        {
            if (id != infoPeople.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(infoPeople);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoPeopleExists(infoPeople.Id))
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
            return View(infoPeople);
        }

        // GET: InfoPeoples/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.infoPeoples == null)
            {
                return NotFound();
            }

            var infoPeople = await _context.infoPeoples
                .FirstOrDefaultAsync(m => m.Id == id);
            if (infoPeople == null)
            {
                return NotFound();
            }

            return View(infoPeople);
        }

        // POST: InfoPeoples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.infoPeoples == null)
            {
                return Problem("Entity set 'PruebaDbContext.infoPeoples'  is null.");
            }
            var infoPeople = await _context.infoPeoples.FindAsync(id);
            if (infoPeople != null)
            {
                _context.infoPeoples.Remove(infoPeople);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoPeopleExists(int id)
        {
          return (_context.infoPeoples?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
