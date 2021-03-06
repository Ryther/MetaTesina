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
    public class AssetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Asset
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Asset.Include(a => a.AssetType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Asset/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Asset
                .Include(a => a.AssetType)
                .SingleOrDefaultAsync(m => m.AssetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // GET: Asset/Create
        public IActionResult Create()
        {
            ViewData["AssetTypeID"] = new SelectList(_context.Set<AssetType>(), "AssetTypeID", "AssetTypeName");
            return View();
        }

        // POST: Asset/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetID,AssetTypeID,AssetName,AssetDescription,AssetPath")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AssetTypeID"] = new SelectList(_context.Set<AssetType>(), "AssetTypeID", "AssetTypeName", asset.AssetTypeID);
            return View(asset);
        }

        // GET: Asset/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Asset.SingleOrDefaultAsync(m => m.AssetID == id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["AssetTypeID"] = new SelectList(_context.Set<AssetType>(), "AssetTypeID", "AssetTypeDescription", asset.AssetTypeID);
            return View(asset);
        }

        // POST: Asset/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetID,AssetTypeID,AssetName,AssetDescription,AssetPath")] Asset asset)
        {
            if (id != asset.AssetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.AssetID))
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
            ViewData["AssetTypeID"] = new SelectList(_context.Set<AssetType>(), "AssetTypeID", "AssetTypeDescription", asset.AssetTypeID);
            return View(asset);
        }

        // GET: Asset/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Asset
                .Include(a => a.AssetType)
                .SingleOrDefaultAsync(m => m.AssetID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Asset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _context.Asset.SingleOrDefaultAsync(m => m.AssetID == id);
            _context.Asset.Remove(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AssetExists(int id)
        {
            return _context.Asset.Any(e => e.AssetID == id);
        }
    }
}
