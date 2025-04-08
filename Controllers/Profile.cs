using System;
    using Township_API.Data;     
    using Township_API.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;



namespace Township_API.Controllers
{
    [Route("api/[controller]")]
        [ApiController]
        public class ProfileController : ControllerBase
        {
            private readonly AppDBContext _context;

            public ProfileController(AppDBContext context)
            {
                _context = context;
            }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            return await _context.Profiles.Include(p => p.user).ToListAsync();
        }


        [HttpPost]
            public async Task<ActionResult<Profile>> CreateProfile(Profile profile)
            {
                _context.Profiles.Add(profile);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProfiles), new { id = profile.ID }, profile);
            }
        }
    }

 
