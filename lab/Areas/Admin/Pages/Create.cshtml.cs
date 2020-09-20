using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using lab.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace lab.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly lab.DAL.Data.ApplicationDbContext _context;

        private readonly IWebHostEnvironment _environment;

        public CreateModel(lab.DAL.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        public IActionResult OnGet()
        {
            ViewData["GroupId"] = new SelectList(_context.MusInstrumentGroups, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public MusInstrument MusInstrument { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MusInstruments.Add(MusInstrument);
            await _context.SaveChangesAsync();

            if (Image != null)
            {
                var fileName = $"{MusInstrument.Id}" + Path.GetExtension(Image.FileName);
                MusInstrument.Image = fileName;

                var path = Path.Combine(_environment.WebRootPath, "images", fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
