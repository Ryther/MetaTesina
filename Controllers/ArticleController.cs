using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MetaTesina.Data;
using MetaTesina.Models;
using MetaTesina.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace MetaTesina.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticleController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Article
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            var articles = await _context.Article
                            .Include(a => a.ApplicationUser)
                            .Include(a => a.ArticleLinkImg)
                            .Include(a => a.ArticleMainImg)
                            .Include(a => a.Category)
                            .Where(a => a.ApplicationUserID == userId)
                            .ToListAsync();
            
            foreach (var article in articles)
            {
                article.ArticleDescription = Convert.ToString(article.ArticleDescription).Substring(0, 10);
                article.ArticleContent = Convert.ToString(article.ArticleContent).Substring(0, 25);
            }

            return View(articles);
        }

        // GET: Article/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.ApplicationUser)
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

        // GET: Article/Show/1
        [AllowAnonymous]
        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.ApplicationUser)
                .Include(a => a.ArticleMainImg)
                .Include(a => a.Category)
                .SingleOrDefaultAsync(m => m.ArticleID == id);

            article.ArticleContent = WebUtility.HtmlDecode(article.ArticleContent);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Article/Create
        public async Task<IActionResult> Create()
        {
            UserHelper userHelper = new UserHelper(_userManager, HttpContext);
            string userId = userHelper.GetUserId().Result;
            string userFirstName = userHelper.GetUserFirstNameAsync().Result;
            
            List<ApplicationUser> user = await _context.Users
                            .Where(u => u.Id == userId)
                            .ToListAsync();

            ViewData["ApplicationUserID"] = new SelectList(user, "Id", "ApplicationUserFirstName");
            ViewData["ArticleLinkImgID"] = new SelectList(_context.Asset, "AssetID", "AssetName");
            ViewData["ArticleMainImgID"] = new SelectList(_context.Asset, "AssetID", "AssetName");
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleID,CategoryID,ArticleLinkImgID,ArticleMainImgID,ApplicationUserID,ArticleCreateDate,ArticleModifyDate,ArticleTitle,ArticleDescription,ArticleContent")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            UserHelper userHelper = new UserHelper(_userManager, HttpContext);
            string userId = userHelper.GetUserId().Result;
            string userFirstName = userHelper.GetUserFirstNameAsync().Result;
            
            List<ApplicationUser> user = await _context.Users
                            .Where(u => u.Id == userId)
                            .ToListAsync();

            ViewData["ApplicationUserID"] = new SelectList(user, "Id", "ApplicationUserFirstName", article.ApplicationUserID);
            ViewData["ArticleLinkImgID"] = new SelectList(_context.Asset, "AssetID", "AssetName", article.ArticleLinkImgID);
            ViewData["ArticleMainImgID"] = new SelectList(_context.Asset, "AssetID", "AssetName", article.ArticleMainImgID);
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName", article.CategoryID);
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
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id", article.ApplicationUserID);
            ViewData["ArticleLinkImgID"] = new SelectList(_context.Asset, "AssetID", "AssetDescription", article.ArticleLinkImgID);
            ViewData["ArticleMainImgID"] = new SelectList(_context.Asset, "AssetID", "AssetDescription", article.ArticleMainImgID);
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryDescription", article.CategoryID);
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleID,CategoryID,ArticleLinkImgID,ArticleMainImgID,ApplicationUserID,ArticleCreateDate,ArticleModifyDate,ArticleTitle,ArticleDescription,ArticleContent")] Article article)
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
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id", article.ApplicationUserID);
            ViewData["ArticleLinkImgID"] = new SelectList(_context.Asset, "AssetID", "AssetDescription", article.ArticleLinkImgID);
            ViewData["ArticleMainImgID"] = new SelectList(_context.Asset, "AssetID", "AssetDescription", article.ArticleMainImgID);
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryDescription", article.CategoryID);
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
                .Include(a => a.ApplicationUser)
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
