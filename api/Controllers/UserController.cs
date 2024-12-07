using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using BCrypt.Net;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEntry>>> GetUserEntry()
        {
            return await _context.UserEntry.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEntry>> GetUserEntry(int id)
        {
            var userEntry = await _context.UserEntry.FindAsync(id);

            if (userEntry == null)
            {
                return NotFound();
            }

            return userEntry;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserEntry(int id, UserEntry userEntry)
        {
            if (id != userEntry.Id)
            {
                return BadRequest();
            }

            userEntry.HashedPassword = BCrypt.Net.BCrypt.HashPassword(userEntry.HashedPassword);

            _context.Entry(userEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEntryExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserEntry>> PostUserEntry(UserEntry userEntry)
        {
            userEntry.HashedPassword = BCrypt.Net.BCrypt.HashPassword(userEntry.HashedPassword);

            _context.UserEntry.Add(userEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserEntry", new { id = userEntry.Id }, userEntry);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEntry(int id)
        {
            var userEntry = await _context.UserEntry.FindAsync(id);
            if (userEntry == null)
            {
                return NotFound();
            }

            _context.UserEntry.Remove(userEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserEntryExists(int id)
        {
            return _context.UserEntry.Any(e => e.Id == id);
        }
    }
}
