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
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Article
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Article.Include(a => a.ArticleLinkImg).Include(a => a.ArticleMainImg).Include(a => a.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Article/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.ArticleLinkImg)
                .Include(a => a.ArticleMainImg)
                .Include(a => a.Category)
                .SingleOrDefaultAsync(m => m.ArticleID == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Article/Create
        public IActionResult Create()
        {
            ViewData["ArticleLinkImgID"] = new SelectList(_context.Set<Asset>(), "AssetID", "AssetID");
            ViewData["ArticleMainImgID"] = new SelectList(_context.Set<Asset>(), "AssetID", "AssetID");
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryDescription");
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleID,CategoryID,ArticleLinkImgID,ArticleMainImgID,AuthorID,ArticleCreateDate,ArticleModifyDate,ArticleTitle,ArticleDescription,ArticleContent")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ArticleLinkImgID"] = new SelectList(_context.Set<Asset>(), "AssetID", "AssetID", article.ArticleLinkImgID);
            ViewData["ArticleMainImgID"] = new SelectList(_context.Set<Asset>(), "AssetID", "AssetID", article.ArticleMainImgID);
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryDescription", article.CategoryID);
            return View(article);
        }

        // GET: Article/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.SingleOrDefaultAsync(m => m.ArticleID == id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["ArticleLinkImgID"] = new SelectList(_context.Set<Asset>(), "AssetID", "AssetID", article.ArticleLinkImgID);
            ViewData["ArticleMainImgID"] = new SelectList(_context.Set<Asset>(), "AssetID", "AssetID", article.ArticleMainImgID);
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryDescription", article.CategoryID);
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleID,CategoryID,ArticleLinkImgID,ArticleMainImgID,AuthorID,ArticleCreateDate,ArticleModifyDate,ArticleTitle,ArticleDescription,ArticleContent")] Article article)
        {
            if (id != article.ArticleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleID))
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
            ViewData["ArticleLinkImgID"] = new SelectList(_context.Set<Asset>(), "AssetID", "AssetID", article.ArticleLinkImgID);
            ViewData["ArticleMainImgID"] = new SelectList(_context.Set<Asset>(), "AssetID", "AssetID", article.ArticleMainImgID);
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryDescription", article.CategoryID);
            return View(article);
        }

        // GET: Article/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.ArticleLinkImg)
                .Include(a => a.ArticleMainImg)
                .Include(a => a.Category)
                .SingleOrDefaultAsync(m => m.ArticleID == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Article.SingleOrDefaultAsync(m => m.ArticleID == id);
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.ArticleID == id);
        }
    }
}
