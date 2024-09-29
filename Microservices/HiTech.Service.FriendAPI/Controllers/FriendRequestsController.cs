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
    public class FriendRequestsController : ControllerBase
    {
        private readonly FriendDbContext _context;

        public FriendRequestsController(FriendDbContext context)
        {
            _context = context;
        }

        // GET: api/FriendRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendRequest>>> GetFriendRequests()
        {
            return await _context.FriendRequests.ToListAsync();
        }

        // GET: api/FriendRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FriendRequest>> GetFriendRequest(int id)
        {
            var friendRequest = await _context.FriendRequests.FindAsync(id);

            if (friendRequest == null)
            {
                return NotFound();
            }

            return friendRequest;
        }

        // PUT: api/FriendRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendRequest(int id, FriendRequest friendRequest)
        {
            if (id != friendRequest.FriendRequestId)
            {
                return BadRequest();
            }

            _context.Entry(friendRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendRequestExists(id))
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

        // POST: api/FriendRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FriendRequest>> PostFriendRequest(FriendRequest friendRequest)
        {
            _context.FriendRequests.Add(friendRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriendRequest", new { id = friendRequest.FriendRequestId }, friendRequest);
        }

        // DELETE: api/FriendRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendRequest(int id)
        {
            var friendRequest = await _context.FriendRequests.FindAsync(id);
            if (friendRequest == null)
            {
                return NotFound();
            }

            _context.FriendRequests.Remove(friendRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FriendRequestExists(int id)
        {
            return _context.FriendRequests.Any(e => e.FriendRequestId == id);
        }
    }
}
