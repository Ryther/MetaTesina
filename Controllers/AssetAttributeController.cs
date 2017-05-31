using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MetaTesina.Data;
using MetaTesina.Models;

namespace MetaTesina.Controllers
{
    public class AssetAttributeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetAttributeController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: AssetAttribute
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AssetAttribute.Include(a => a.AssetType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AssetAttribute/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAttribute = await _context.AssetAttribute
                .Include(a => a.AssetType)
                .SingleOrDefaultAsync(m => m.AssetAttributeID == id);
            if (assetAttribute == null)
            {
                return NotFound();
            }

            return View(assetAttribute);
        }

        // GET: AssetAttribute/Create
        public IActionResult Create()
        {
            ViewData["AssetTypeID"] = new SelectList(_context.Set<AssetType>(), "AssetTypeID", "AssetTypeDescription");
            return View();
        }

        // POST: AssetAttribute/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetAttributeID,AssetTypeID,AssetTypeName,AssetTypeDescription")] AssetAttribute assetAttribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetAttribute);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AssetTypeID"] = new SelectList(_context.Set<AssetType>(), "AssetTypeID", "AssetTypeDescription", assetAttribute.AssetTypeID);
            return View(assetAttribute);
        }

        // GET: AssetAttribute/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAttribute = await _context.AssetAttribute.SingleOrDefaultAsync(m => m.AssetAttributeID == id);
            if (assetAttribute == null)
            {
                return NotFound();
            }
            ViewData["AssetTypeID"] = new SelectList(_context.Set<AssetType>(), "AssetTypeID", "AssetTypeDescription", assetAttribute.AssetTypeID);
            return View(assetAttribute);
        }

        // POST: AssetAttribute/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetAttributeID,AssetTypeID,AssetTypeName,AssetTypeDescription")] AssetAttribute assetAttribute)
        {
            if (id != assetAttribute.AssetAttributeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetAttribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetAttributeExists(assetAttribute.AssetAttributeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["AssetTypeID"] = new SelectList(_context.Set<AssetType>(), "AssetTypeID", "AssetTypeDescription", assetAttribute.AssetTypeID);
            return View(assetAttribute);
        }

        // GET: AssetAttribute/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAttribute = await _context.AssetAttribute
                .Include(a => a.AssetType)
                .SingleOrDefaultAsync(m => m.AssetAttributeID == id);
            if (assetAttribute == null)
            {
                return NotFound();
            }

            return View(assetAttribute);
        }

        // POST: AssetAttribute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetAttribute = await _context.AssetAttribute.SingleOrDefaultAsync(m => m.AssetAttributeID == id);
            _context.AssetAttribute.Remove(assetAttribute);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AssetAttributeExists(int id)
        {
            return _context.AssetAttribute.Any(e => e.AssetAttributeID == id);
        }
    }
}
