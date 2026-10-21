
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWebSite.Models;
using AspNetCoreWebSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Operations;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace AspNetCoreMvc.Controllers
{

    public class AIImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public AIImagesController(ApplicationDbContext context, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        // GET: AIIMAGES
        [Authorize(Roles = "admin, member")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.AIImage.ToListAsync());
        }

        // GET: AIIMAGES/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aiImage = await _context.AIImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aiImage == null)
            {
                return NotFound();
            }

            return View(aiImage);
        }

        // GET: AIIMAGES/Create
        [Authorize(Roles = "admin, member")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AIIMAGES/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, member")]
        //public async Task<IActionResult> Create([Bind("Id,Prompt,MessageContent")] AIImage aiImage, UploadFile uploadFile)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(aiImage);
        //    }

        //    if (uploadFile.File != null)
        //    {
        //        var fileName = Path.GetFileName(uploadFile.File.FileName);
        //        var imagesDir = Path.Combine(_hostingEnv.WebRootPath, "images");
        //        if (!Path.Exists(imagesDir))
        //        {
        //            Directory.CreateDirectory(imagesDir);
        //        }
        //        var filePath = Path.Combine(imagesDir, fileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await uploadFile.File.CopyToAsync(fileStream);
        //        }
        //    }

        //    _context.Add(aiImage);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> Create([Bind("Id, Prompt, MessageContent")] AIImage aiImage, UploadFile uploadFile)
        {
            if (!ModelState.IsValid)
            {
                return View(aiImage);
            }

            if (uploadFile.File == null)
            {
                // TODO: throw invalid exception or something
                return View(aiImage);
            }

            _context.Add(aiImage);

            aiImage.FileName = Path.GetFileName(uploadFile.File.FileName);
            aiImage.FileExtension = Path.GetExtension(uploadFile.File.FileName);
            aiImage.UploadDate = DateTime.Now;
            aiImage.FileSize = uploadFile.File.Length;
            aiImage.UserName = User.Identity?.Name;

            await _context.SaveChangesAsync();

            var filePath = Path.Combine(_hostingEnv.WebRootPath, aiImage.StoragePath);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await uploadFile.File.CopyToAsync(fileStream);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        // GET: AIIMAGES/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aiImage = await _context.AIImage.FindAsync(id);
            if (aiImage == null)
            {
                return NotFound();
            }
            return View(aiImage);
        }

        // POST: AIIMAGES/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,PostDate,UserName,TopicTitle,MessageContent")] AIImage aiImage)
        {
            if (id != aiImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aiImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AIImageExists(aiImage.Id))
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
            return View(aiImage);
        }

        // GET: AIIMAGES/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aiImage = await _context.AIImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aiImage == null)
            {
                return NotFound();
            }

            return View(aiImage);
        }

        // POST: AIIMAGES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var aiImage = await _context.AIImage.FindAsync(id);
            if (aiImage != null)
            {
                _context.AIImage.Remove(aiImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AIImageExists(int? id)
        {
            return _context.AIImage.Any(e => e.Id == id);
        }

        public async Task<IActionResult> IncreaseLike(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aiImage = await _context.AIImage.FindAsync(id);
            if (aiImage == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    aiImage.Like++;

                    _context.Update(aiImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AIImageExists(aiImage.Id))
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