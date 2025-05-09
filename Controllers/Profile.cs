using System;
using Township_API.Data;
using Township_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Township_API.Models.commonTypes;



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
            return await _context.Profiles.IgnoreAutoIncludes().ToListAsync();
        }

        [HttpGet("GetProfile/{id}")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfileByID(int id)
        {
            return await _context.Profiles.Where (p=>p.ID ==id).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Profile>> CreateProfile(Profile profile)
        {
            try
            {
                var existingprofile = await _context.Profiles.FindAsync(0);
                if (existingprofile != null)
                {
                    return BadRequest("Profile Exists.");
                }
                var existingProf = await _context.Profiles.Where(p=>p.profilename==profile.profilename).ToListAsync();
                if (existingProf != null)
                {
                    if (existingProf.Count>0) 
                        return BadRequest("Profile name already Exists.");
                }



                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblProfile ON");
                _context.Add(profile);
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblProfile OFF");
                await _context.SaveChangesAsync();

                return Ok(new { message = $"{profile.ID} Profile Created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPost("UpdateProfile/{id}")]
        public async Task<ActionResult<Profile>> UpdateProfile(int id, Profile profile)
        {
            try
            {
                if (id != profile.ID)
                {
                    return BadRequest("Profile ID mismatch.");
                }

                var existingprofile = await _context.Profiles.FindAsync(id);
                if (existingprofile == null)
                {
                    return NotFound();
                }

                var existingprof = await _context.Profiles.Where(p => p.ID != id && p.profilename == profile.profilename).ToListAsync();
                if (existingprof != null)
                {
                    if (existingprof.Count > 0)
                        return BadRequest("Profile already exists!");
                }

                existingprofile.profilename = profile.profilename;
                existingprofile.uid = profile.uid;
                existingprofile.updatedon = DateTime.Now;
                existingprofile.updatedby = profile.updatedby;
                existingprofile.isactive = profile.isactive;
                existingprofile.isdeleted = profile.isdeleted;

                _context.Entry(existingprofile).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return Ok(new { message = $"{id} Profile updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Error: " + ex.Message.ToString());
                return BadRequest("Error :" + ex.Message.ToString());

            }

        }


        [HttpGet("GetProfileDetails/{ID}")]
        public async Task<ActionResult<IEnumerable<ProfileDetails>>> GetProfileDetails(string Id)
        {
            return await _context.ProfileDetails.Where(p => p.profileid.ToString() == Id.ToString()).ToListAsync();
        }

        [HttpPost("AddUpdateProfileDetail")]
        public async Task<IActionResult> AddUpdateProfileDetail([FromBody] ProfileDetails Obj)
        {
            if (Obj == null)  
                return BadRequest("No Data provided");

            //using var transaction = await _context.Database.BeginTransactionAsync();
            try
            { 

                var existingobj = await _context.ProfileDetails
                        .FirstOrDefaultAsync(p => p.profileid == Obj.profileid && p.moduleId == Obj.moduleId);

                    if (existingobj != null)
                    {
                    existingobj.moduleId = Obj.moduleId;
                    existingobj.profileid = Obj.profileid;
                    existingobj.CanInsert = Obj.CanInsert;
                    existingobj.CanView = Obj.CanView;

                    existingobj.CanUpdate = Obj.CanUpdate;
                    existingobj.CanDelete = Obj.CanDelete;
                    existingobj.CanView = Obj.CanView;
                    existingobj.isactive = Obj.isactive;
                    existingobj.updatedby = Obj.updatedby;
                    existingobj.updatedon = Obj.updatedon;
                    existingobj.isdeleted = Obj.isdeleted;

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblProfileDetails ON");
                        _context.ProfileDetails.Add(Obj);

                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblProfileDetails OFF");
                    await _context.SaveChangesAsync();
                    }
                
                //   await transaction.CommitAsync();

                return Ok(new { message = $"Profile Detail processed successfully" });
            }
            catch (Exception ex)
            {
                //   await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }

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
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblProfile ON");
                        _context.ProfileDetails.Add(objID);

                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblProfile OFF");
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


