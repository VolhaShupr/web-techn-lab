using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab.DAL.Data;
using lab.DAL.Entities;

namespace lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusInstrumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MusInstrumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MusInstruments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusInstrument>>> GetMusInstruments(int? groupId)
        {
            return await _context.MusInstruments
                .Where(i => !groupId.HasValue || i.GroupId.Equals(groupId.Value))?
                .ToListAsync();
        }

        // GET: api/MusInstruments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MusInstrument>> GetMusInstrument(int id)
        {
            var musInstrument = await _context.MusInstruments.FindAsync(id);

            if (musInstrument == null)
            {
                return NotFound();
            }

            return musInstrument;
        }

        // PUT: api/MusInstruments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusInstrument(int id, MusInstrument musInstrument)
        {
            if (id != musInstrument.Id)
            {
                return BadRequest();
            }

            _context.Entry(musInstrument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusInstrumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MusInstruments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MusInstrument>> PostMusInstrument(MusInstrument musInstrument)
        {
            _context.MusInstruments.Add(musInstrument);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMusInstrument", new { id = musInstrument.Id }, musInstrument);
        }

        // DELETE: api/MusInstruments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MusInstrument>> DeleteMusInstrument(int id)
        {
            var musInstrument = await _context.MusInstruments.FindAsync(id);
            if (musInstrument == null)
            {
                return NotFound();
            }

            _context.MusInstruments.Remove(musInstrument);
            await _context.SaveChangesAsync();

            return musInstrument;
        }

        private bool MusInstrumentExists(int id)
        {
            return _context.MusInstruments.Any(e => e.Id == id);
        }
    }
}
