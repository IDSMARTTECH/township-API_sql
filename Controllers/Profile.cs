﻿using System;
using Township_API.Data;
using Township_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Township_API.Models.commonTypes;
using System.Linq;
using System.Text.Json;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;


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
            return await _context.Profiles.Where(p => p.ID == id).ToListAsync();
        }


        [HttpPost("AddUpdateProfile")]
        public async Task<ActionResult<Profile>> AddUpdateProfile(Profile profile)
        {
            try
            {
                if (profile == null)
                    return BadRequest("No Data provided");

                if (profile.ID == 0)
                {
                    var _existingprofile = await _context.Profiles.Where(p => p.ProfileName == profile.ProfileName).ToListAsync();
                    if (_existingprofile != null)
                    {
                        if (_existingprofile.Count > 0)
                            return BadRequest("Profile already exists!");
                    }

                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblProfile ON");
                    _context.Profiles.Add(profile);
                    await _context.SaveChangesAsync();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblProfile OFF");

                    return Ok(profile);
                }

                if (profile.ID > 0)
                {
                    var _exists = await _context.Profiles.Where(p => p.ID != profile.ID && p.ProfileName == profile.ProfileName).ToListAsync();
                    if (_exists != null)
                    {
                        if (_exists.Count > 0)
                            return BadRequest("Profile already exists!");
                    }

                    var existingobj = await _context.Profiles.Where(p => p.ID == profile.ID).FirstOrDefaultAsync();

                    if (existingobj != null) { 
                    existingobj.ProfileName = profile.ProfileName;
                    existingobj.Companyid = profile.Companyid;
                    existingobj.UpdatedOn = profile.UpdatedOn;
                    existingobj.UpdatedBy = profile.UpdatedBy;
                    existingobj.isActive = profile.isActive;
                    existingobj.isDeleted = profile.isDeleted;

                    await _context.SaveChangesAsync();
                    }
                    return Ok(existingobj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Error: " + ex.Message.ToString());
                return BadRequest("Error :" + ex.Message.ToString());

            }
            return Ok(null);
        }

        //[HttpGet("GetProfileDetails/{id}")]
        //public async Task<ActionResult> GetProfileDetails(string id)
        //{
        //    // Load data into memory
        //    var _profileDtls = _context.ProfileDetails.Where(p => p.profileid.ToString() == id).ToList();
        //    //var _profile = _context.Profiles.ToList();
        //    //var _module = _context.Modules.ToList();

        //    // Perform join in memory
        //    var result = from _pd in _profileDtls
        //                 select (_pd.module.ModuleID, _pd.module.ModuleName, _pd.module.Viewreadonly, _pd.CanInsert, _pd.CanUpdate, _pd.CanInsert, _pd.CanView);

        //    return Ok(JsonSerializer.Serialize(result));
        //}
        [HttpGet("GetProfileDDetails/{id}")]
        public async Task<ActionResult<IEnumerable<ProfileDetails>>> GetProfileDetails(string id)
        {
            try
            { 
                List<ProfileDetails> profileDetails = new List<ProfileDetails>();               
                // Perform join in memory
                var result = await _context.Modules.ToListAsync();
                if (result != null)
                {
                    int i=1;
                    if (result.Count > 0)
                    {
                        Profile _profile = await _context.Profiles.Where(u => u.ID == Convert.ToInt16(id)).FirstOrDefaultAsync();
                        ProfileDetails P;
                        foreach (var res in result)
                        {   
                            P = new ProfileDetails();
                            _module M = new _module();
                            P.id = i;
                            P.moduleId = res.ModuleID;
                            P.profileid =Convert.ToInt16( id);
                            P.profile= _profile;
                            M.ModuleID = res.ModuleID;
                            M.ModuleName = res.ModuleName;
                            M.Viewreadonly = res.Viewreadonly;
                            P.module = M;
                            P.updatedby = 0;
                            P.updatedon =DateTime.Now;
                            P.isdeleted = false;
                            var nr = await _context.ProfileDetails.Where(u =>u.profileid== Convert.ToInt16(id) && u.moduleId.ToString() == res.ModuleID.ToString()).FirstOrDefaultAsync();
                            if (nr != null)
                            {
                                P.CanUpdate = nr.CanUpdate;
                                P.CanDelete = nr.CanDelete;
                                P.CanView = nr.CanView;
                                P.isactive = nr.isactive;
                                
                            }
                            else
                            {
                                P.CanUpdate = false;
                                P.CanDelete = false;
                                P.CanView = false;
                                P.isactive = false;
                            }
                            i++;
                            profileDetails.Add(P);
                        }

                       // return Ok(profileDetails);
                    } 
                }
                // return Ok(null);
                return Ok(profileDetails);
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
                        //var existingProfilDtls = await _context.ProfileDetails.FindAsync(objID.id);
                        if (existingobj != null)
                        {

                            existingobj.moduleId = objID.moduleId;
                            existingobj.profileid = objID.profileid;
                            existingobj.CanInsert = objID.CanInsert;
                            existingobj.CanView = objID.CanView;

                            existingobj.CanUpdate = objID.CanUpdate;
                            existingobj.CanDelete = objID.CanDelete;
                            existingobj.CanView = objID.CanView;
                            existingobj.isactive = objID.isactive;
                            existingobj.updatedby = objID.updatedby;
                            existingobj.updatedon = objID.updatedon;
                            existingobj.isdeleted = objID.isdeleted;
                        }
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblProfileDetails ON");
                        _context.ProfileDetails.Add(objID);

                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblProfileDetails OFF");
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

        [HttpGet("GetModuleNames/{isViewOnly}")]
        public async Task<ActionResult<IEnumerable<_module>>> GetModuleNames(bool isViewOnly = false)
        {
            if (isViewOnly)
                return await _context.Modules.Where(p => p.Viewreadonly == isViewOnly).IgnoreAutoIncludes().ToListAsync();

            return await _context.Modules.IgnoreAutoIncludes().ToListAsync();
        }
    }
}


