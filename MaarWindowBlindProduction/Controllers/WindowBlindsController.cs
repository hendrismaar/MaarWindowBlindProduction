using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaarWindowBlindProduction.Data;
using MaarWindowBlindProduction.Models;
using MaarWindowBlindProduction.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

//TODO: add viewmodel 2 placeorder (ask teacher)
// css and logo to pages
// write documentation

namespace MaarWindowBlindProduction.Controllers
{
    public class WindowBlindsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public WindowBlindsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WindowBlinds
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(OrderState));
            }
            return View(await _context.WindowBlind.ToListAsync());
        }
        // GET: WindowBlinds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(OrderState));
            }
            if (id == null || _context.WindowBlind == null)
            {
                return NotFound();
            }

            var windowBlind = await _context.WindowBlind
                .FirstOrDefaultAsync(m => m.Id == id);
            if (windowBlind == null)
            {
                return NotFound();
            }

            return View(windowBlind);
        }

        // GET: WindowBlinds/Create
        public IActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(OrderState));
            }
            return View();
        }

        // POST: WindowBlinds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,Pattern,ClothReady,FrameReady,ProductPackaged,DeliveryStatus")] Order windowBlind)
        {
            if (ModelState.IsValid)
            {
                _context.Add(windowBlind);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(windowBlind);
        }

        // GET: WindowBlinds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(OrderState));
            }
            if (id == null || _context.WindowBlind == null)
            {
                return NotFound();
            }

            var windowBlind = await _context.WindowBlind.FindAsync(id);
            if (windowBlind == null)
            {
                return NotFound();
            }
            return View(windowBlind);
        }

        // POST: WindowBlinds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,Pattern,ClothReady,FrameReady,ProductPackaged,DeliveryStatus")] Order windowBlind)
        {
            if (id != windowBlind.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(windowBlind);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WindowBlindExists(windowBlind.Id))
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
            return View(windowBlind);
        }

        // GET: WindowBlinds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(OrderState));
            }
                if (id == null || _context.WindowBlind == null)
            {
                return NotFound();
            }

            var windowBlind = await _context.WindowBlind
                .FirstOrDefaultAsync(m => m.Id == id);
            if (windowBlind == null)
            {
                return NotFound();
            }

            return View(windowBlind);
        }

        // POST: WindowBlinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WindowBlind == null)
            {
                return Problem("Entity set 'ApplicationDbContext.WindowBlind'  is null.");
            }
            var windowBlind = await _context.WindowBlind.FindAsync(id);
            if (windowBlind != null)
            {
                _context.WindowBlind.Remove(windowBlind);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> PlaceOrder()
        {
            var model = new CreateOrderViewModel();
            model.Order = new Order();
            model.Patterns = CreatePatternSelectList();
            return View(model);
        }

        private List<SelectListItem> CreatePatternSelectList(int? selected = null)
        {
            var selectList = new SelectList(_context.Set<Pattern>(), "Id", "Name", selected).ToList();
            return selectList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CreateOrderViewModel windowBlind)
        {
            var chosenPattern = _context.Patterns.Find(windowBlind.PatternId);
            if (chosenPattern != null)
            {
                ModelState.ClearValidationState("Order.Pattern");
                ModelState.MarkFieldValid("Order.Pattern");
                windowBlind.Order.Pattern = chosenPattern;
            }

            if (ModelState.IsValid)
            {
                _context.Add(windowBlind.Order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(OrderState));
            }
            return View(windowBlind);
        }

        // GET: WindowBlinds/OrderState
        public async Task<IActionResult> OrderState(string search)
        {
            var windowBlinds = _context.WindowBlind.Include(e => e.Pattern).AsQueryable();
            //return View(await applicationDbContext.ToListAsync());
            //var windowBlinds = from order in _context.WindowBlind
            //                   select order;

            if (!String.IsNullOrEmpty(search))
            {
                windowBlinds = windowBlinds.Where(s => s.OrderId!.Contains(search));
            }
            return View(await windowBlinds.ToListAsync());
        }

        public async Task<IActionResult> WorkerList()
        {
            var windowBlind = _context.WindowBlind.Where(e => e.DeliveryStatus != true);

            var roles = new[]
           {
                "Admin",
                "Manufacturer",
                "Clothier",
                "Packager",
                "Deliverer"
            };
            // User is not authenticated, redirect to login in the same area
            foreach (var role in roles)
            {
                if (User.IsInRole(role))
                {
                    return View(windowBlind);
                }
            }
            return Redirect("/Identity/Account/Login");
        }


        // GET: WindowBlinds/ClothierEdit
        public async Task<IActionResult> ClothierEdit(int? id)
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Clothier"))
            {
                return RedirectToAction(nameof(WorkerList));
            }
            if (id == null || _context.WindowBlind == null)
            {
                return NotFound();
            }

            var windowBlind = await _context.WindowBlind.FindAsync(id);
            if (windowBlind == null)
            {
                return NotFound();
            }
            return View(windowBlind);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClothierEdit(int id, [Bind("Id,ClothReady,FrameReady,ProductPackaged,DeliveryStatus")] WindowBlindViewModel windowBlind)
        {
            if (id != windowBlind.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentData = await _context.WindowBlind.FindAsync(id);
                    currentData.ClothReady = windowBlind.ClothReady;
                    _context.Entry(currentData).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WindowBlindExists(windowBlind.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(WorkerList));
            }
            return View(windowBlind);
        }

        // GET: WindowBlinds/ManufacturerEdit
        public async Task<IActionResult> ManufacturerEdit(int? id)
        {
            if (id == null || _context.WindowBlind == null)
            {
                return NotFound();
            }

            var windowBlind = await _context.WindowBlind.FindAsync(id);
            if (windowBlind == null)
            {
                return NotFound();
            }
            if (!User.IsInRole("Admin") && !User.IsInRole("Manufacturer"))
            {
                return RedirectToAction(nameof(WorkerList));
            } 
                return View(windowBlind);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManufacturerEdit(int id, [Bind("Id,ClothReady,FrameReady,ProductPackaged,DeliveryStatus")] WindowBlindViewModel windowBlind)
        {
            if (id != windowBlind.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentData = await _context.WindowBlind.FindAsync(id);
                    currentData.FrameReady = windowBlind.FrameReady;
                    _context.Entry(currentData).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WindowBlindExists(windowBlind.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(WorkerList));
            }
            return View(windowBlind);
        }

        // GET: WindowBlinds/PackagerEdit
        public async Task<IActionResult> PackagerEdit(int? id)
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Packager"))
            {
                return RedirectToAction(nameof(WorkerList));
            }
            if (id == null || _context.WindowBlind == null)
            {
                return NotFound();
            }

            var windowBlind = await _context.WindowBlind.FindAsync(id);
            if (windowBlind == null)
            {
                return NotFound();
            }
            return View(windowBlind);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PackagerEdit(int id, [Bind("Id,ClothReady,FrameReady,ProductPackaged,DeliveryStatus")] WindowBlindViewModel windowBlind)
        {
            if (id != windowBlind.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentData = await _context.WindowBlind.FindAsync(id);
                    currentData.ProductPackaged = windowBlind.ProductPackaged;
                    _context.Entry(currentData).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WindowBlindExists(windowBlind.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(WorkerList));
            }
            return View(windowBlind);
        }

        // GET: WindowBlinds/DelivererEdit
        public async Task<IActionResult> DelivererEdit(int? id)
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Deliverer"))
            {
                return RedirectToAction(nameof(WorkerList));
            }
            if (id == null || _context.WindowBlind == null)
            {
                return NotFound();
            }

            var windowBlind = await _context.WindowBlind.FindAsync(id);
            if (windowBlind == null)
            {
                return NotFound();
            }
            return View(windowBlind);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DelivererEdit(int id, [Bind("Id,ClothReady,FrameReady,ProductPackaged,DeliveryStatus")] WindowBlindViewModel windowBlind)
        {
            if (id != windowBlind.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentData = await _context.WindowBlind.FindAsync(id);
                    currentData.DeliveryStatus = windowBlind.DeliveryStatus;
                    _context.Entry(currentData).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WindowBlindExists(windowBlind.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(WorkerList));
            }
            return View(windowBlind);
        }

        private bool WindowBlindExists(int id)
        {
          return _context.WindowBlind.Any(e => e.Id == id);
        }
    }
}