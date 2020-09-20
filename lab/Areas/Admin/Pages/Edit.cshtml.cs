using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace lab.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly lab.DAL.Data.ApplicationDbContext _context;

        private IWebHostEnvironment _environment;

        public EditModel(lab.DAL.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        [BindProperty]
        public MusInstrument MusInstrument { get; set; }
        
        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MusInstrument = await _context.MusInstruments
                .Include(m => m.Group).FirstOrDefaultAsync(m => m.Id == id);

            if (MusInstrument == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.MusInstrumentGroups, "Id", "Name");
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Image != null)
            {

                var fileName = $"{MusInstrument.Id}" + Path.GetExtension(Image.FileName);

                MusInstrument.Image = fileName;

                var path = Path.Combine(_environment.WebRootPath, "images", fileName);

                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
            }

            _context.Attach(MusInstrument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusInstrumentExists(MusInstrument.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MusInstrumentExists(int id)
        {
            return _context.MusInstruments.Any(e => e.Id == id);
        }
    }
}
