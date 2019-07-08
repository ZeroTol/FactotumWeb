using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactotumModels;
using FactotumModels.Context;

namespace FactotumWeb.Controllers
{
    public class EDashBoardFactsController : Controller
    {
        private readonly FtDbContext _context;

        public EDashBoardFactsController(FtDbContext context)
        {
            _context = context;
        }

        // GET: EDashBoardFacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dashboard.ToListAsync());
        }

        // GET: EDashBoardFacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eDashBoardFact = await _context.Dashboard
                .FirstOrDefaultAsync(m => m.FactId == id);
            if (eDashBoardFact == null)
            {
                return NotFound();
            }

            return View(eDashBoardFact);
        }

        // GET: EDashBoardFacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EDashBoardFacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FactId,FactTitle,FactLink,FactCreateDate,FactRating,FactHashTags,FactLastRateDate")] EDashBoardFact eDashBoardFact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eDashBoardFact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eDashBoardFact);
        }

        // GET: EDashBoardFacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eDashBoardFact = await _context.Dashboard.FindAsync(id);
            if (eDashBoardFact == null)
            {
                return NotFound();
            }
            return View(eDashBoardFact);
        }

        // POST: EDashBoardFacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FactId,FactTitle,FactLink,FactCreateDate,FactRating,FactHashTags,FactLastRateDate")] EDashBoardFact eDashBoardFact)
        {
            if (id != eDashBoardFact.FactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eDashBoardFact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EDashBoardFactExists(eDashBoardFact.FactId))
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
            return View(eDashBoardFact);
        }

        // GET: EDashBoardFacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eDashBoardFact = await _context.Dashboard
                .FirstOrDefaultAsync(m => m.FactId == id);
            if (eDashBoardFact == null)
            {
                return NotFound();
            }

            return View(eDashBoardFact);
        }

        // POST: EDashBoardFacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eDashBoardFact = await _context.Dashboard.FindAsync(id);
            _context.Dashboard.Remove(eDashBoardFact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EDashBoardFactExists(int id)
        {
            return _context.Dashboard.Any(e => e.FactId == id);
        }
    }
}
