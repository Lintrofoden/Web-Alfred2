using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            if (HttpContext.Request.Cookies.ContainsKey("Title") && HttpContext.Request.Cookies.ContainsKey("Content") && HttpContext.Request.Cookies.ContainsKey("Image") && HttpContext.Request.Cookies.ContainsKey("DatePublished"))
            {
                ViewData["Title"] = HttpContext.Request.Cookies["Title"];
                ViewData["Content"] = HttpContext.Request.Cookies["Content"];
                ViewData["Image"] = HttpContext.Request.Cookies["Image"];
                //ViewData["DatePublished"] = DateTime.ParseExact(HttpContext.Request.Cookies["DatePublished"], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ViewData["DatePublished"] = HttpContext.Request.Cookies["DatePublished"];
            }

            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Author,Title,Content,CategoryId,Image,DatePublished")] Post post, string save)
        {
            if(save == "yes" && post.Title != null && post.Content != null && post.Image != null && post.DatePublished != null)
            {
                HttpContext.Response.Cookies.Append("Title", post.Title.ToString());
                HttpContext.Response.Cookies.Append("Content", post.Content.ToString());
                HttpContext.Response.Cookies.Append("Image", post.Image.ToString());
                HttpContext.Response.Cookies.Append("DatePublished", post.DatePublished.ToString());
                //ViewData["Title"] = HttpContext.Request.Cookies["Title"].ToString();
                //ViewData["Content"] = HttpContext.Request.Cookies["Content"].ToString();
                //ViewData["Image"] = HttpContext.Request.Cookies["Image"].ToString();
                //ViewData["DatePublished"] = HttpContext.Request.Cookies["DatePublished"].ToString();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", post.CategoryId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Author,Title,Content,CategoryId,Image,DatePublished")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", post.CategoryId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
