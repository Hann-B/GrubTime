using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrubTime.Models;

namespace GrubTime.Controllers
{
    [Produces("application/json")]
    [Route("api/Profiles")]
    public class ProfilesController : Controller
    {
        private readonly GrubTimeContext _context;

        public ProfilesController(GrubTimeContext context)
        {
            _context = context;
        }

        // GET: api/Profiles
        [HttpGet]
        public IEnumerable<UserProfile> GetProfiles()
        {
            return _context.Profiles;
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProfile = await _context.Profiles.SingleOrDefaultAsync(m => m.Id == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
        }

        // PUT: api/Profiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProfile([FromRoute] int id, [FromBody] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userProfile.Id)
            {
                return BadRequest();
            }

            _context.Entry(userProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(id))
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

        // POST: api/Profiles
        [HttpPost]
        public async Task<IActionResult> PostUserProfile([FromBody] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Profiles.Add(userProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserProfile", new { id = userProfile.Id }, userProfile);
        }

        // DELETE: api/Profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProfile = await _context.Profiles.SingleOrDefaultAsync(m => m.Id == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(userProfile);
            await _context.SaveChangesAsync();

            return Ok(userProfile);
        }

        private bool UserProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}