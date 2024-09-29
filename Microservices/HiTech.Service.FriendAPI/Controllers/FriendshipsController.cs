using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HiTech.Service.FriendAPI.Data;
using HiTech.Service.FriendAPI.Entities;

namespace HiTech.Service.FriendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private readonly FriendDbContext _context;

        public FriendshipsController(FriendDbContext context)
        {
            _context = context;
        }

        // GET: api/Friendships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetFriendships()
        {
            return await _context.Friendships.ToListAsync();
        }

        // GET: api/Friendships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friendship>> GetFriendship(int id)
        {
            var friendship = await _context.Friendships.FindAsync(id);

            if (friendship == null)
            {
                return NotFound();
            }

            return friendship;
        }

        // PUT: api/Friendships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendship(int id, Friendship friendship)
        {
            if (id != friendship.FriendshipId)
            {
                return BadRequest();
            }

            _context.Entry(friendship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendshipExists(id))
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

        // POST: api/Friendships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Friendship>> PostFriendship(Friendship friendship)
        {
            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriendship", new { id = friendship.FriendshipId }, friendship);
        }

        // DELETE: api/Friendships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendship(int id)
        {
            var friendship = await _context.Friendships.FindAsync(id);
            if (friendship == null)
            {
                return NotFound();
            }

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FriendshipExists(int id)
        {
            return _context.Friendships.Any(e => e.FriendshipId == id);
        }
    }
}
