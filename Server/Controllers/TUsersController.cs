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
    public class TUsersController : ControllerBase
    {
        private readonly DataContext _context;

        public TUsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TUser>>> GetTUsers()
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TUsers.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                return await _context.TUsers.Where(x => x.LoginUserId == loginuserId.ToString()).ToListAsync();
            }
            return NotFound();
        }

        // GET: api/TUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TUser>> GetTUser(int id)
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TUsers.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                var tUser = await _context.TUsers.FindAsync(id);
                if (tUser == null)
                {
                    return NotFound();
                }
                return tUser;
            }
            return NotFound();
        }

        // PUT: api/TUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTUser(int id, TUser tUser)
        {
            if (id != tUser.UserId)
            {
                return BadRequest();
            }
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                tUser.LoginUserId = loginuserId.ToString();
            }
            _context.Entry(tUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TUserExists(id))
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

        // POST: api/TUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TUser>> PostTUser(TUser tUser)
        {
          if (_context.TUsers == null)
          {
              return Problem("Entity set 'DataContext.TUsers'  is null.");
          }
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                tUser.LoginUserId = loginuserId.ToString();
            }
            _context.TUsers.Add(tUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTUser", new { id = tUser.UserId }, tUser);
        }

        // DELETE: api/TUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTUser(int id)
        {
            if (Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, out Guid loginuserId))
            {
                if (_context.TUsers.Where(x => x.LoginUserId == loginuserId.ToString()) == null)
                {
                    return NotFound();
                }
                var tUser = await _context.TUsers.FindAsync(id);
                if (tUser == null)
                {
                    return NotFound();
                }
                _context.TUsers.Remove(tUser);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return NotFound();
        }

        private bool TUserExists(int id)
        {
            return (_context.TUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
