using System;
using Township_API.Data;
using Township_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;



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
        [HttpGet("GetProfileDetails/{ID}")]
        public async Task<ActionResult<IEnumerable<ProfileDetails>>> GetProfileDetails(string Id)
        {
            return await _context.ProfileDetails.Where(p => p.profileid.ToString() == Id.ToString()).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Profile>> CreateProfile(Profile profile)
        {
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProfiles), new { id = profile.ID }, profile);
        }

        [HttpPost("AddUpdateProfileDetails")]
        public async Task<IActionResult> AddUpdateProfileDetails([FromBody] List<ProfileDetails> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

            //using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.ProfileDetails
                        .FirstOrDefaultAsync(p => p.profileid == objID.profileid && p.moduleId == objID.moduleId);

                    if (existingobj != null)
                    {
                        // var existingLandOwner = await _service.UpdateLandownerAsync(objID.ID, existingobj);
                        var existingProfilDtls = await _context.ProfileDetails.FindAsync(objID.id);
                        if (existingProfilDtls == null)
                        {
                            return NotFound();
                        }

                        existingProfilDtls.moduleId = objID.moduleId;
                        existingProfilDtls.profileid = objID.profileid;
                        existingProfilDtls.CanInsert = objID.CanInsert;
                        existingProfilDtls.CanView = objID.CanView;

                        existingProfilDtls.CanUpdate = objID.CanUpdate;
                        existingProfilDtls.CanDelete = objID.CanDelete;
                        existingProfilDtls.CanView = objID.CanView;
                        existingProfilDtls.isactive = objID.isactive;
                        existingProfilDtls.updatedby = objID.updatedby;
                        existingProfilDtls.updatedon = objID.updatedon;
                        existingProfilDtls.isdeleted = objID.isdeleted;

                        await   _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.ProfileDetails.Add(objID);
                        await _context.SaveChangesAsync();
                    }
                }

                //   await transaction.CommitAsync();

                return Ok(new { message = $"Records of Profile Details processed successfully" });
            }
            catch (Exception ex)
            {
                //   await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }

        }

    }
}


