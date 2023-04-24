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
    public class TTrialsController : ControllerBase
    {
        private readonly DataContext _context;

        public TTrialsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TTrials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TTrial>>> GetTTrials()
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TTrials.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                return await _context.TTrials.Where(x => x.LoginUserId == loginuserId.ToString()).ToListAsync();
            }
            return NotFound();
        }

        // GET: api/TTrials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TTrial>> GetTTrial(int id)
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TTrials.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                var tTrial = await _context.TTrials.FindAsync(id);
                if (tTrial == null)
                {
                    return NotFound();
                }
                return tTrial;
            }
            return NotFound();
        }

        // PUT: api/TTrials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTTrial(int id, TTrial tTrial)
        {
            if (id != tTrial.TrialId)
            {
                return BadRequest();
            }
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                tTrial.LoginUserId = loginuserId.ToString();
            }
            _context.Entry(tTrial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TTrialExists(id))
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

        // POST: api/TTrials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TTrial>> PostTTrial(TTrial tTrial)
        {
          if (_context.TTrials == null)
          {
              return Problem("Entity set 'DataContext.TTrials'  is null.");
          }
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                tTrial.LoginUserId = loginuserId.ToString();
            }
            _context.TTrials.Add(tTrial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTTrial", new { id = tTrial.TrialId }, tTrial);
        }

        // DELETE: api/TTrials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTTrial(int id)
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TTrials.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                var tTrial = await _context.TTrials.FindAsync(id);
                if (tTrial == null)
                {
                    return NotFound();
                }
                _context.TTrials.Remove(tTrial);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }

        private bool TTrialExists(int id)
        {
            return (_context.TTrials?.Any(e => e.TrialId == id)).GetValueOrDefault();
        }
    }
}
