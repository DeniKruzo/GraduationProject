using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GraduationProject.Data;
using GraduationProject.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GraduationProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryOrdersController : Controller
    {
        private readonly GraduationDbContext _context;

        public CategoryOrdersController(GraduationDbContext context)
        {
            _context = context;
        }

        // GET: CategoryOrders
        public async Task<IActionResult> Index()
        {
              return _context.CategoryOrder != null ? 
                          View(await _context.CategoryOrder.ToListAsync()) :
                          Problem("Entity set 'GraduationDbContext.CategoryOrder'  is null.");
        }

        // GET: CategoryOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryOrder == null)
            {
                return NotFound();
            }

            var categoryOrder = await _context.CategoryOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryOrder == null)
            {
                return NotFound();
            }

            return View(categoryOrder);
        }

        // GET: CategoryOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] CategoryOrder categoryOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryOrder);
        }

        // GET: CategoryOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryOrder == null)
            {
                return NotFound();
            }

            var categoryOrder = await _context.CategoryOrder.FindAsync(id);
            if (categoryOrder == null)
            {
                return NotFound();
            }
            return View(categoryOrder);
        }

        // POST: CategoryOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] CategoryOrder categoryOrder)
        {
            if (id != categoryOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryOrderExists(categoryOrder.Id))
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
            return View(categoryOrder);
        }

        // GET: CategoryOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryOrder == null)
            {
                return NotFound();
            }

            var categoryOrder = await _context.CategoryOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryOrder == null)
            {
                return NotFound();
            }

            return View(categoryOrder);
        }

        // POST: CategoryOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryOrder == null)
            {
                return Problem("Entity set 'GraduationDbContext.CategoryOrder'  is null.");
            }
            var categoryOrder = await _context.CategoryOrder.FindAsync(id);
            if (categoryOrder != null)
            {
                _context.CategoryOrder.Remove(categoryOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryOrderExists(int id)
        {
          return (_context.CategoryOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
