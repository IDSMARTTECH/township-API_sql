
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using Township_API.Data;
using Township_API.Models;
using static Township_API.Models.commonTypes;

namespace Township_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly AppDBContext _context;
        public CompanyController(AppDBContext context)
        {
            _context = context;
        }

        [HttpPost("UpdateCompany/{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] Company updatedOBJ)
        {
            try
            {
                if (id != updatedOBJ.id)
                {
                    return BadRequest("Company ID mismatch.");
                }
                var existingOBJ = await _context.Companies.FindAsync(id);
                if (existingOBJ == null)
                {
                    return NotFound();
                }
                var existsOBJ = await _context.Companies.Where(p => p.companyName == updatedOBJ.companyName && p.id != updatedOBJ.id).ToListAsync();
                if (existsOBJ != null)
                {
                    if (existsOBJ.Count > 0)
                        return BadRequest("Company Name is already Exists.");
                } 
                existingOBJ.comanyCode = updatedOBJ.comanyCode;
                existingOBJ.companyName = updatedOBJ.companyName;
                existingOBJ.city = updatedOBJ.city;
                existingOBJ.address = updatedOBJ.address;
                existingOBJ.GSTnumber = updatedOBJ.GSTnumber;
                existingOBJ.pannumber = updatedOBJ.pannumber;
                existingOBJ.mobileNo = updatedOBJ.mobileNo;
                existingOBJ.landLine = updatedOBJ.landLine;
                existingOBJ.ContactPerson = updatedOBJ.ContactPerson;
                existingOBJ.ContactMobile = updatedOBJ.ContactMobile;
                existingOBJ.ValidFromDate = updatedOBJ.ValidFromDate;
                existingOBJ.ValidToDate = updatedOBJ.ValidToDate;
                existingOBJ.isactive = updatedOBJ.isactive;
                existingOBJ.isdeleted = updatedOBJ.isdeleted; 
                existingOBJ.updatedby = updatedOBJ.updatedby;
                existingOBJ.updatedon = updatedOBJ.updatedon;

                _context.Entry(existingOBJ).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(existingOBJ);
            }
            catch (Exception ex)
            {
                //     await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany([FromBody] Company obj)
        {
            try
            {
                var existingOBJ = await _context.Companies.FindAsync(0);
                if (existingOBJ != null)
                {
                    return BadRequest("Company data Exists.");
                } 
                var existOBJ = await _context.Companies.Where(p => p.companyName.ToString() == obj.companyName.ToString()).ToListAsync();
                if (existOBJ != null)
                {
                    if (existOBJ.Count > 0)
                        return BadRequest("Company name already exists!");
                }

                // Turn IDENTITY_INSERT ON
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Company ON");
                _context.Add(obj);

                // Turn IDENTITY_INSERT ON
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Company OFF");
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Company processed successfully" });
            }
            catch (Exception ex)
            {
                //     await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPost("DeleteCompany/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                if (id <=0)
                {
                    return BadRequest("Company ID mismatch.");
                }
                var existingOBJ = await _context.Companies.FindAsync(id);
                if (existingOBJ == null)
                {
                    return NotFound();
                } 
                existingOBJ.isdeleted = true;
                existingOBJ.updatedby = 1;
                existingOBJ.updatedon = DateTime.Now;

                _context.Entry(existingOBJ).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(existingOBJ);
            }
            catch (Exception ex)
            {
                //     await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                  var OBJs = await _context.Companies.Where(p=>p.isactive==true && p.isdeleted ==false).IgnoreAutoIncludes().OrderByDescending(p => p.id).ToListAsync();


                return Ok(OBJs);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetCompany/{ID}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            { 
                var OBJs = await _context.Companies.Where(n =>n.id==id && n.isactive==true && n.isdeleted==false).IgnoreAutoIncludes().OrderByDescending(p => p.id).ToListAsync(); 
                return Ok(OBJs);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
         

        [HttpPost("bulkSaveCompany")]
        public async Task<IActionResult> BulkSave([FromBody] List<Company> Obj)
        {

            //using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.Companies
                        .FirstOrDefaultAsync(v => v.companyName == updatedObj.companyName);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.comanyCode = updatedObj.comanyCode;
                        existingOBJ.companyName = updatedObj.companyName;
                        existingOBJ.city = updatedObj.city;
                        existingOBJ.address = updatedObj.address;
                        existingOBJ.GSTnumber = updatedObj.GSTnumber;
                        existingOBJ.pannumber = updatedObj.pannumber;
                        existingOBJ.mobileNo = updatedObj.mobileNo;
                        existingOBJ.landLine = updatedObj.landLine;
                        existingOBJ.ContactPerson = updatedObj.ContactPerson;
                        existingOBJ.ContactMobile = updatedObj.ContactMobile;
                        existingOBJ.ValidFromDate = updatedObj.ValidFromDate;
                        existingOBJ.ValidToDate = updatedObj.ValidToDate;
                        existingOBJ.isactive = updatedObj.isactive;
                        existingOBJ.isdeleted = updatedObj.isdeleted;
                        existingOBJ.updatedby = updatedObj.updatedby;
                        existingOBJ.updatedon = DateTime.Now;
                        await _context.SaveChangesAsync();
                        //  await transaction.CommitAsync();
                    }
                    else
                    {
                        // Turn IDENTITY_INSERT ON
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT company ON");
                        _context.Companies.Add(updatedObj);
                        //  await transaction.CommitAsync();
                        // Turn IDENTITY_INSERT ON
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT company OFF");
                        await _context.SaveChangesAsync();
                    }
                }


                return Ok(new { message = $"company data processed successfully" });
            }
            catch (Exception ex)
            {
                //     await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly AppDBContext _context;
        public ProjectController(AppDBContext context)
        {
            _context = context;
        }

        [HttpPost("UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project updatedOBJ)
        {
            try
            {
                if (id != updatedOBJ.id)
                {
                    return BadRequest("Project ID mismatch.");
                }
                var existingOBJ = await _context.Projects.FindAsync(id);
                if (existingOBJ == null)
                {
                    return NotFound();
                }
                var existsOBJ = await _context.Projects.Where(p => p.ProjectName == updatedOBJ.ProjectName && p.id != updatedOBJ.id).ToListAsync();
                if (existsOBJ != null)
                {
                    if (existsOBJ.Count > 0)
                        return BadRequest("Project Name is already Exists.");
                }
                existingOBJ.ProjectCode = updatedOBJ.ProjectCode; 
                existingOBJ.ProjectName = updatedOBJ.ProjectName;
                existingOBJ.CompanyID = updatedOBJ.CompanyID;     
                existingOBJ.isactive = updatedOBJ.isactive;
                existingOBJ.isdeleted = updatedOBJ.isdeleted;
                existingOBJ.updatedby = updatedOBJ.updatedby;
                existingOBJ.updatedon = updatedOBJ.updatedon;

                _context.Entry(existingOBJ).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(existingOBJ);
            }
            catch (Exception ex)
            {
                //     await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                if (id<=0)
                {
                    return BadRequest("Project ID mismatch.");
                }
                var existingOBJ = await _context.Projects.FindAsync(id);
                if (existingOBJ == null)
                {
                    return NotFound();
                }
                 
                existingOBJ.isdeleted = true;
                existingOBJ.updatedby = 1;
                existingOBJ.updatedon = DateTime.Now;

                _context.Entry(existingOBJ).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(existingOBJ);
            }
            catch (Exception ex)
            {
                //     await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPost("AddProject")]
        public async Task<IActionResult> AddProject([FromBody] Project obj)
        {
            try
            {
                var existingOBJ = await _context.Projects.FindAsync(0);
                if (existingOBJ != null)
                {
                    return BadRequest("Project data Exists.");
                }
                var existOBJ = await _context.Projects.Where(p => p.ProjectName.ToString() == obj.ProjectName.ToString()).ToListAsync();
                if (existOBJ != null)
                {
                    if (existOBJ.Count > 0)
                        return BadRequest("Project name already exists!");
                }
                var project = new Project
                {
                    ProjectCode = obj.ProjectCode,
                    ProjectName = obj.ProjectName,
                    CompanyID = obj.CompanyID,
                    isactive = obj.isactive,
                    isdeleted = obj.isdeleted,
                    createdby = obj.createdby,
                    createdon = obj.createdon,
                    updatedby = obj.updatedby,
                    updatedon = obj.updatedon
                };
 

                 //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Project ON");
                _context.Add(project);
                //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Project OFF");
                // Turn IDENTITY_INSERT ON
                await _context.SaveChangesAsync();
                return Ok(new { message = $"Project processed successfully"}); ;
            }
            catch (Exception ex)
            {
                //     await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComapnyProjects(int companyID)
        {
            try
            {
                object OBJs;

                if(companyID>0) 
                    OBJs = await _context.Projects.Where(p =>  p.isactive == true && p.isdeleted == false && p.CompanyID == companyID).OrderByDescending(p => p.id).ToListAsync();
                else
                    OBJs = await _context.Projects.Where(p => p.isactive  ==true &&   p.isdeleted == false).IgnoreAutoIncludes().OrderByDescending(p => p.id).ToListAsync();

                return Ok(OBJs);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpGet("GetProject/{ID}")]
        public async Task<IActionResult> GetProject(int id)
        {
            try
            {
                var OBJs = await _context.Projects.Where(n => n.id == id && n.isactive == true  && n.isdeleted == false).IgnoreAutoIncludes().OrderByDescending(p => p.id).ToListAsync();
                
                return Ok(OBJs);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost("bulkSaveProject")]
        public async Task<IActionResult> BulkSave([FromBody] List<Project> Obj)
        {

            //using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.Projects
                        .FirstOrDefaultAsync(v => v.ProjectName == updatedObj.ProjectName ||  v.id == updatedObj.id);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.ProjectCode = updatedObj.ProjectCode; 
                        existingOBJ.ProjectName = updatedObj.ProjectName;
                        existingOBJ.CompanyID = updatedObj.CompanyID;
                        existingOBJ.isactive = updatedObj.isactive;
                        existingOBJ.isdeleted = updatedObj.isdeleted;
                        existingOBJ.updatedby = updatedObj.updatedby;
                        existingOBJ.updatedon = updatedObj.updatedon;
                        existingOBJ.updatedon = DateTime.Now;
                        await _context.SaveChangesAsync();
                        //  await transaction.CommitAsync();
                    }
                    else
                    {
                        // Turn IDENTITY_INSERT ON
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Project ON");
                        _context.Projects.Add(updatedObj);
                        //  await transaction.CommitAsync();
                        // Turn IDENTITY_INSERT ON
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Project OFF");
                        await _context.SaveChangesAsync();
                    }
                }


                return Ok(new { message = $"Project data processed successfully" });
            }
            catch (Exception ex)
            {
                //     await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

}

