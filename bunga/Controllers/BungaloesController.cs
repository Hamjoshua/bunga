using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bunga.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace bunga.Models
{
    public class BungaloesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public BungaloesController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Rating(int bungalo_id, int rating_value)
        {
            if(ModelState.IsValid)
            {
                var rating = _context.Rating.Include(r => r.Бунгало).Include(r => r.Покупатель)
                                        .Where(r => r.Бунгало.Id_бунгало == bungalo_id && r.Покупатель.UserName == User.Identity.Name)
                                        .FirstOrDefault();
                if (rating == null)
                {
                    rating = new Rating();
                    rating.Бунгало = await _context.Bungalo.FindAsync(bungalo_id);
                    rating.Покупатель = await _userManager.GetUserAsync(User);
                    _context.Add(rating);
                    await _context.SaveChangesAsync();
                }
                rating.Оценка = rating_value;
                _context.Update(rating);
                await _context.SaveChangesAsync();
            }
            
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public Bungalo GetBungaloWithIncludedUser(int? id)
        {
            var bungaloes = _context.Bungalo.Include(b => b.Сдающий);
            var bungalo = bungaloes.Where(b => b.Id_бунгало == id).First();
            return bungalo;
        }

        public async Task<List<Bungalo>> GetListOfBungaloWithIncludedUser()
        {
            return await _context.Bungalo.Include(b => b.Сдающий).ToListAsync();
        }

        // GET: Bungaloes
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string address = "", string members = "0", string wifi = null,
            string min_price = "0", string max_price = "99999999")
        {
            return View(await _context.Bungalo.Include(b => b.Сдающий).Where(b =>
                                                                       (String.IsNullOrEmpty(address) || b.Адрес.Contains(address))
                                                                       && (String.IsNullOrEmpty(members) || b.Количество_человек >= (int.Parse(members)))
                                                                       && (String.IsNullOrEmpty(wifi) || b.Wi_fi.Equals(wifi))
                                                                       && (String.IsNullOrEmpty(min_price) || b.Стоимость_сутки >= int.Parse(min_price))
                                                                       && (String.IsNullOrEmpty(max_price) || b.Стоимость_сутки <= int.Parse(max_price)))
                                                                       .ToListAsync());
        }

        // GET: Bungaloes/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bungalo = GetBungaloWithIncludedUser(id);
            if (bungalo == null)
            {
                return NotFound();
            }

            return View(bungalo);
        }

        [Authorize(Roles = RoleNames.Deliver)]
        // GET: Bungaloes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bungaloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_бунгало,Название,Описание,Адрес,Количество_человек,Wi_fi,Id_сдающий,Стоимость_сутки")] Bungalo bungalo, IFormFileCollection bungaloImages)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                bungalo.Сдающий = user;
                _context.Add(bungalo);
                System.Diagnostics.Debug.WriteLine("UserId is " + bungalo.Сдающий.Id);
                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine("After 'SaveChangesAsync' UserId is " + bungalo.Сдающий.Id);
                bungalo = await _context.Bungalo.FindAsync(bungalo.Id_бунгало);
                System.Diagnostics.Debug.WriteLine("Now UserId is " + bungalo.Сдающий.Id);

                await SaveFileCollection(bungalo, bungaloImages);

                return RedirectToAction(nameof(Index));
            }
            return View(bungalo);
        }

        public async Task SaveFileCollection(Bungalo bungalo, IFormFileCollection files)
        {    
            string directory = "wwwroot/static/bungaloes/" + bungalo.Id_бунгало.ToString();
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            else
            {
                ClearDirectory(directory, files);
            }

            foreach (var file in files)
            {
                if(file.FileName != "image-missing-icon.png" && file.Length > 0)
                {
                    string path = directory + "/" + file.FileName;
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                
            }
        }

        private bool isFileInFilePaths(List<string> paths, FileInfo file)
        {
            foreach (string path in paths)
            {
                if (file.FullName.Contains(path))
                {
                    return true;
                }
            }
            return false;
        }

        public string ClearFileName(string filename)
        {
            return filename.Split("/")[filename.Split("/").Length - 1];
        }

        public void ClearDirectory(string directory, IFormFileCollection formFiles)
        {           
            DirectoryInfo dirInfo = new DirectoryInfo(directory);
            List<string> file_paths = new List<string>();

            foreach(var formFile in formFiles)
            {
                file_paths.Add(formFile.FileName);
            }

            foreach(FileInfo file in dirInfo.GetFiles())
            {                
                if(!isFileInFilePaths(file_paths, file))
                {
                    file.Delete();
                }
            }
        }

        [Authorize(Roles = RoleNames.Deliver)]
        // GET: Bungaloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bungalo = GetBungaloWithIncludedUser(id);
            if (bungalo == null)
            {
                return NotFound();
            }
            if (bungalo.Сдающий.UserName != User.Identity.Name)
            {                
                return NotFound();
            }
            System.Diagnostics.Debug.WriteLine(string.Format("{0}", string.Join("', '", bungalo.GetImages())));
            ViewData["Images"] = string.Format("{0}", string.Join("', '", bungalo.GetImages()));
            return View(bungalo);
        }

        // POST: Bungaloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = RoleNames.Deliver)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_бунгало,Название,Описание,Адрес,Количество_человек,Wi_fi,Id_сдающий,Стоимость_сутки")] Bungalo bungalo, IFormFileCollection bungaloImages)
        {
            if (id != bungalo.Id_бунгало)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bungalo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BungaloExists(bungalo.Id_бунгало))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                await SaveFileCollection(bungalo, bungaloImages);
                return RedirectToAction(nameof(Index));
            }
            return View(bungalo);
        }

        // GET: Bungaloes/Delete/5
        
        [Authorize(Roles = RoleNames.EditorsOfBungalo)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bungalo = GetBungaloWithIncludedUser(id);
            if (bungalo == null)
            {
                return NotFound();
            }

            return View(bungalo);
        }

        // POST: Bungaloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = RoleNames.EditorsOfBungalo)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bungalo = await _context.Bungalo.FindAsync(id);
            _context.Bungalo.Remove(bungalo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BungaloExists(int id)
        {
            return _context.Bungalo.Any(e => e.Id_бунгало == id);
        }
    }
}
