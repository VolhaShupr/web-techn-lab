using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab.DAL.Data;
using lab.DAL.Entities;

namespace lab.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly lab.DAL.Data.ApplicationDbContext _context;

        public DeleteModel(lab.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MusInstrument MusInstrument { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MusInstrument = await _context.MusInstruments.FindAsync(id);

            if (MusInstrument != null)
            {
                _context.MusInstruments.Remove(MusInstrument);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
