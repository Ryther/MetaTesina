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
    public class AssetTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetTypeController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: AssetType
        public async Task<IActionResult> Index()
        {
            return View(await _context.AssetType.ToListAsync());
        }

        // GET: AssetType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetType = await _context.AssetType
                .SingleOrDefaultAsync(m => m.AssetTypeID == id);
            if (assetType == null)
            {
                return NotFound();
            }

            return View(assetType);
        }

        // GET: AssetType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssetType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetTypeID,AssetTypeName,AssetTypeDescription")] AssetType assetType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(assetType);
        }

        // GET: AssetType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetType = await _context.AssetType.SingleOrDefaultAsync(m => m.AssetTypeID == id);
            if (assetType == null)
            {
                return NotFound();
            }
            return View(assetType);
        }

        // POST: AssetType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetTypeID,AssetTypeName,AssetTypeDescription")] AssetType assetType)
        {
            if (id != assetType.AssetTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetTypeExists(assetType.AssetTypeID))
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
            return View(assetType);
        }

        // GET: AssetType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetType = await _context.AssetType
                .SingleOrDefaultAsync(m => m.AssetTypeID == id);
            if (assetType == null)
            {
                return NotFound();
            }

            return View(assetType);
        }

        // POST: AssetType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetType = await _context.AssetType.SingleOrDefaultAsync(m => m.AssetTypeID == id);
            _context.AssetType.Remove(assetType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AssetTypeExists(int id)
        {
            return _context.AssetType.Any(e => e.AssetTypeID == id);
        }
    }
}
