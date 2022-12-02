﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaarWindowBlindProduction.Data;
using MaarWindowBlindProduction.Models;

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
              return View(await _context.WindowBlind.ToListAsync());
        }

        // GET: WindowBlinds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            return View();
        }

        // POST: WindowBlinds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Address,PatternNumber,ClothReady,FrameReady,ProductPackaged,DeliveryStatus")] WindowBlind windowBlind)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,PatternNumber,ClothReady,FrameReady,ProductPackaged,DeliveryStatus")] WindowBlind windowBlind)
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

        public IActionResult PlaceOrder()
        {
            return View();
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

        // GET: WindowBlinds/OrderState
        public async Task<IActionResult> OrderState(int? id)
        {
            var windowBlind = await _context.WindowBlind.FindAsync(id);
            return View(windowBlind);
        }

        private bool WindowBlindExists(int id)
        {
          return _context.WindowBlind.Any(e => e.Id == id);
        }
    }
}