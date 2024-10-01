using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HiTech.Service.GroupAPI.Data;
using HiTech.Service.GroupAPI.Entities;

namespace HiTech.Service.GroupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinRequestsController : ControllerBase
    {
        private readonly GroupDbContext _context;

        public JoinRequestsController(GroupDbContext context)
        {
            _context = context;
        }

        // GET: api/JoinRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JoinRequest>>> GetJoinRequests()
        {
            return await _context.JoinRequests.ToListAsync();
        }

        // GET: api/JoinRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JoinRequest>> GetJoinRequest(int id)
        {
            var joinRequest = await _context.JoinRequests.FindAsync(id);

            if (joinRequest == null)
            {
                return NotFound();
            }

            return joinRequest;
        }

        // PUT: api/JoinRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJoinRequest(int id, JoinRequest joinRequest)
        {
            if (id != joinRequest.JoinRequestId)
            {
                return BadRequest();
            }

            _context.Entry(joinRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JoinRequestExists(id))
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

        // POST: api/JoinRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JoinRequest>> PostJoinRequest(JoinRequest joinRequest)
        {
            _context.JoinRequests.Add(joinRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJoinRequest", new { id = joinRequest.JoinRequestId }, joinRequest);
        }

        // DELETE: api/JoinRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJoinRequest(int id)
        {
            var joinRequest = await _context.JoinRequests.FindAsync(id);
            if (joinRequest == null)
            {
                return NotFound();
            }

            _context.JoinRequests.Remove(joinRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JoinRequestExists(int id)
        {
            return _context.JoinRequests.Any(e => e.JoinRequestId == id);
        }
    }
}
