
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWebSite.Models;
using AspNetCoreWebSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Operations;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace AspNetCoreMvc.Controllers
{

    public class DiscussionForumsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public DiscussionForumsController(ApplicationDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        // GET: DISCUSSIONFORUMS
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscussionForum.ToListAsync());
        }

        // GET: DISCUSSIONFORUMS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionforum = await _context.DiscussionForum
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discussionforum == null)
            {
                return NotFound();
            }

            return View(discussionforum);
        }

        // GET: DISCUSSIONFORUMS/Create
        [Authorize(Roles = "admin, user")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DISCUSSIONFORUMS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Create([Bind("Id,PostDate,UserName,TopicTitle,MessageContent")] DiscussionForum discussionforum, UploadFile uploadFile)
        {
            if (uploadFile.File != null)
            {
                var fileName = Path.GetFileName(uploadFile.File.FileName);
                var filePath = Path.Combine(_hostingEnv.WebRootPath, "images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadFile.File.CopyToAsync(fileStream);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(discussionforum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discussionforum);
        }

        [Authorize(Roles = "admin")]
        // GET: DISCUSSIONFORUMS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionforum = await _context.DiscussionForum.FindAsync(id);
            if (discussionforum == null)
            {
                return NotFound();
            }
            return View(discussionforum);
        }

        // POST: DISCUSSIONFORUMS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,PostDate,UserName,TopicTitle,MessageContent")] DiscussionForum discussionforum)
        {
            if (id != discussionforum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussionforum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionForumExists(discussionforum.Id))
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
            return View(discussionforum);
        }

        // GET: DISCUSSIONFORUMS/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionforum = await _context.DiscussionForum
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discussionforum == null)
            {
                return NotFound();
            }

            return View(discussionforum);
        }

        // POST: DISCUSSIONFORUMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var discussionforum = await _context.DiscussionForum.FindAsync(id);
            if (discussionforum != null)
            {
                _context.DiscussionForum.Remove(discussionforum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionForumExists(int? id)
        {
            return _context.DiscussionForum.Any(e => e.Id == id);
        }

        public async Task<IActionResult> IncreaseLike(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var discussionForum = await _context.DiscussionForum.FindAsync(id);
            if (discussionForum == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    discussionForum.Like++;

                    _context.Update(discussionForum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionForumExists(discussionForum.Id))
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
            return RedirectToAction(nameof(Index));
        }
    }
}