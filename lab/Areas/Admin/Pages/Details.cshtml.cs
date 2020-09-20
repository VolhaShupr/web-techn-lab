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
    public class DetailsModel : PageModel
    {
        private readonly lab.DAL.Data.ApplicationDbContext _context;

        public DetailsModel(lab.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
