using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactotumModels;
using FactotumModels.Context;
using FactotumWeb.Repository;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace FactotumWeb.Controllers
{
    [Authorize]
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
            //return View(await _context.Dashboard.ToListAsync());
            var xxx = await _context.Dashboard.ToListAsync();

            //https://localhost:44360/api/message/dashboard/byrating
            ViewData["Guid"] = Guid.NewGuid();
            string DomainName = HttpContext.Request.Path;
            string Domain = HttpContext.Request.Host.Host;
            //var model = await MessageRepository.GetDashBoardMessages("https://localhost:5001/api/message/dashboard/byrating");
            var model = await MessageRepository.GetDashBoardMessages("https://localhost:5001/api/EDashBoardFacts/");

            return View(model);
        }

        // GET: EDashBoardFacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eDashBoardFact = await _context.Dashboard
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,FactId,FactRating,FactCreateDate,FactLastRateDate,FactTitle,FactLink,FactHashTags")] EDashBoardFact eDashBoardFact)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        //"https://factotumwebapi.azurewebsites.net/api/message/dashboard/byrating"
                        //client.BaseAddress = new Uri("https://factotumwebapi.azurewebsites.net");

                        client.BaseAddress = new Uri("https://localhost:5001");
                        var result = await client.PostAsJsonAsync("/api/EDashBoardFacts", eDashBoardFact);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                return RedirectToAction(nameof(Index));

                //_context.Add(eDashBoardFact);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FactId,FactRating,FactCreateDate,FactLastRateDate,FactTitle,FactLink,FactHashTags")] EDashBoardFact eDashBoardFact)
        {
            if (id != eDashBoardFact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        //"https://factotumwebapi.azurewebsites.net/api/message/dashboard/byrating"
                        //client.BaseAddress = new Uri("https://factotumwebapi.azurewebsites.net");

                        client.BaseAddress = new Uri("https://localhost:5001");
                        var result = await client.PutAsJsonAsync($"/api/EDashBoardFacts/{eDashBoardFact.Id}", eDashBoardFact);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                //try
                //{
                //    _context.Update(eDashBoardFact);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!EDashBoardFactExists(eDashBoardFact.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}

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
                .FirstOrDefaultAsync(m => m.Id == id);
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
            //_context.Dashboard.Remove(eDashBoardFact);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EDashBoardFactExists(int id)
        {
            return _context.Dashboard.Any(e => e.Id == id);
        }
    }
}
