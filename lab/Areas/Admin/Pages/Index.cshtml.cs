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
    public class IndexModel : PageModel
    {
        private readonly lab.DAL.Data.ApplicationDbContext _context;

        public IndexModel(lab.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MusInstrument> MusInstrument { get;set; }

        public async Task OnGetAsync()
        {
            MusInstrument = await _context.MusInstruments
                .Include(m => m.Group).ToListAsync();
        }
    }
}
