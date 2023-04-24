using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ozarin.Server.Data;
using Ozarin.Shared;

namespace Ozarin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TTrialSeatsController : ControllerBase
    {
        private readonly DataContext _context;

        public TTrialSeatsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TTrialSeats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TTrialSeat>>> GetTTrialSeats()
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TTrialSeats.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                return await _context.TTrialSeats.Where(x => x.LoginUserId == loginuserId.ToString()).ToListAsync();
            }
            return NotFound();
        }

        // GET: api/TTrialSeats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TTrialSeat>> GetTTrialSeat(int id)
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TTrialSeats.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                var tTrialSeat = await _context.TTrialSeats.FindAsync(id);
                if (tTrialSeat == null)
                {
                    return NotFound();
                }
                return tTrialSeat;
            }
            return NotFound();
        }

        // PUT: api/TTrialSeats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTTrialSeat(int id, TTrialSeat tTrialSeat)
        {
            if (id != tTrialSeat.TrialSeatId)
            {
                return BadRequest();
            }
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                tTrialSeat.LoginUserId = loginuserId.ToString();
            }
            _context.Entry(tTrialSeat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TTrialSeatExists(id))
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

        // POST: api/TTrialSeats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TTrialSeat>> PostTTrialSeat(TTrialSeat tTrialSeat)
        {
          if (_context.TTrialSeats == null)
          {
              return Problem("Entity set 'DataContext.TTrialSeats'  is null.");
          }
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                tTrialSeat.LoginUserId = loginuserId.ToString();
            }
            _context.TTrialSeats.Add(tTrialSeat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTTrialSeat", new { id = tTrialSeat.TrialSeatId }, tTrialSeat);
        }

        // DELETE: api/TTrialSeats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTTrialSeat(int id)
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TTrialSeats.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                var tTrialSeat = await _context.TTrialSeats.FindAsync(id);
                if (tTrialSeat == null)
                {
                    return NotFound();
                }
                _context.TTrialSeats.Remove(tTrialSeat);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }

        private bool TTrialSeatExists(int id)
        {
            return (_context.TTrialSeats?.Any(e => e.TrialSeatId == id)).GetValueOrDefault();
        }
    }
}
