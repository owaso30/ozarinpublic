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
    public class TTablesController : ControllerBase
    {
        private readonly DataContext _context;

        public TTablesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TTable>>> GetTTables()
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TTables.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                return await _context.TTables.Where(x => x.LoginUserId == loginuserId.ToString()).ToListAsync();
            }
            return NotFound();
        }

        // GET: api/TTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TTable>> GetTTable(int id)
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TTables.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                var tTable = await _context.TTables.FindAsync(id);
                if (tTable == null)
                {
                    return NotFound();
                }
                return tTable;
            }
            return NotFound();
        }

        // PUT: api/TTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTTable(int id, TTable tTable)
        {
            if (id != tTable.TableId)
            {
                return BadRequest();
            }
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                tTable.LoginUserId = loginuserId.ToString();
            }
            _context.Entry(tTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TTableExists(id))
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

        // POST: api/TTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TTable>> PostTTable(TTable tTable)
        {
          if (_context.TTables == null)
          {
              return Problem("Entity set 'DataContext.TTables'  is null.");
          }
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                tTable.LoginUserId = loginuserId.ToString();
            }
            _context.TTables.Add(tTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTTable", new { id = tTable.TableId }, tTable);
        }

        // DELETE: api/TTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTTable(int id)
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TTables.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                var tTable = await _context.TTables.FindAsync(id);
                if (tTable == null)
                {
                    return NotFound();
                }
                _context.TTables.Remove(tTable);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }

        private bool TTableExists(int id)
        {
            return (_context.TTables?.Any(e => e.TableId == id)).GetValueOrDefault();
        }
    }
}
