using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FactotumModels;
using FactotumModels.Context;
using Microsoft.AspNetCore.Authorization;
using FactotumModels.Services;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FactotumWeb.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly FtDbContext _context;

        private IUserService _userService;
        public UsersController(FtDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var xxx = User.Identity.IsAuthenticated;

            return View(await _context.Users.ToListAsync());
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]User userParam)
        {
            string DomainName = HttpContext.Request.Path;
            string Domain = HttpContext.Request.Host.Host;

            var user = await _userService.Authenticate(userParam.Username, userParam.Password);
            
            // example how to use the request with Authorization
            using (var client = new HttpClient())
            {
                try
                {
                    //client.BaseAddress = new Uri("https://factotumwebapi.azurewebsites.net");
                    client.BaseAddress = new Uri("https://localhost:44347");
                    //client.DefaultRequestHeaders.Add("Authorization", "test:test");
                    client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Authorization", "test:test");
                //https://localhost:44347/api/FakeApi/GetAllAuthor
                    var result = await client.GetStringAsync("/api/FakeApi/GetAllAuthor");
                }
                catch (Exception)
                {
                    return null;
                }
            }

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // GET: Users/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Username,Password")] User user)
        {
            if (id != user.Id)
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
                        //https://localhost:44375/users
                        client.BaseAddress = new Uri("https://localhost:44347");
                        var result = await client.PostAsJsonAsync("/authenticate", user);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

                //try
                //{
                //    _context.Update(user);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
