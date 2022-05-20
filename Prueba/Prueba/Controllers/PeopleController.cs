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
    public class PeopleController : Controller
    {
        private readonly PruebaDbContext _context;

        public PeopleController(PruebaDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
              return _context.Peoples != null ? 
                          View(await _context.Peoples.ToListAsync()) :
                          Problem("Entity set 'PruebaDbContext.Peoples'  is null.");
            
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peoples == null)
            {
                return NotFound();
            }

            var people = await _context.Peoples
                .FirstOrDefaultAsync(m => m.Idpersons == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idpersons,Identificacion,Nombres,Apellidos,FechNacimiento")] People people)
        {
            if (ModelState.IsValid)
            {
                var lstUsuarios = _context.Peoples.Where(x => x.Identificacion == people.Identificacion).ToList();
                if (lstUsuarios != null && lstUsuarios.Count > 0)
                {
                    TempData["alert"] = "El documento ingresado ya se encuentra asociado a otro usuario";
                    return View(people);
                }
                else {
                    _context.Add(people);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); }
            }
            return View(people);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peoples == null)
            {
                return NotFound();
            }

            var people = await _context.Peoples.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idpersons,Identificacion,Nombres,Apellidos,FechNacimiento")] People people)
        {
            if (id != people.Idpersons)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(people);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeopleExists(people.Idpersons))
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
            return View(people);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peoples == null)
            {
                return NotFound();
            }

            var people = await _context.Peoples
                .FirstOrDefaultAsync(m => m.Idpersons == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peoples == null)
            {
                return Problem("Entity set 'PruebaDbContext.Peoples'  is null.");
            }
            var people = await _context.Peoples.FindAsync(id);
            if (people != null)
            {
                _context.Peoples.Remove(people);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeopleExists(int id)
        {
          return (_context.Peoples?.Any(e => e.Idpersons == id)).GetValueOrDefault();
        }
    }
}
