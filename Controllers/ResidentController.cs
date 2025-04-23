using Microsoft.AspNetCore.Mvc;
using Township_API.Models;
using Township_API.Services;
using Township_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : Controller
    {
        private readonly AppDBContext _context;
        public ResidentController(AppDBContext context)
        {
            _context = context;
        }

        // PUT: api/products/5
        [HttpPut("{UpdateResident}")]
        public async Task<IActionResult> UpdateResident(int id, [FromBody] PrimaryResident updatedResident)
        {
            if (id != updatedResident.ID)
            {
                return BadRequest("Resident ID mismatch.");
            }

            //var existingResident = await _service.UpdatePrimaryResidentAsync(updatedResident.ID, updatedResident);
            var existingResident = await _context.PrimaryResidents.FindAsync(updatedResident.ID);
            if (existingResident == null)
            {
                return NotFound();
            }

            _context.Entry(existingResident).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingResident);
        }


        [HttpPost("AddResident")]
        public async Task<IActionResult> AddResident([FromBody] PrimaryResident obj)
        {
            var existingResident = await _context.PrimaryResidents.FindAsync(0);
            if (existingResident != null)
            {
                return BadRequest("Resident Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllResidents()
        {
            var Residents = await _context.PrimaryResidents.ToListAsync();
            return Ok(Residents);
        }

        [HttpPost("{AddPrimaryResidents}")]
        public async Task<IActionResult> AddPrimaryResidents([FromBody] List<PrimaryResident> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

            using var transaction = await _context.Database.BeginTransactionAsync();

            // Turn IDENTITY_INSERT ON
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PrimaryResident ON");
            

            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.PrimaryResidents
                        .FirstOrDefaultAsync(p => p.ID == objID.ID);

                    if (existingobj != null)
                    {
                        //var existingPrimaryResident = await _service.UpdatePrimaryResidentAsync(objID.ID, existingobj);
                        var existingPrimaryResident = await _context.PrimaryResidents.FindAsync(objID.ID);
                        if (existingPrimaryResident == null)
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        _context.PrimaryResidents.Add(objID);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                // Turn IDENTITY_INSERT OFF
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PrimaryResident  OFF");

                return Ok(new { message = $"{Obj.Count} PrimaryResident processed successfully" });
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }



    [Route("api/[controller]")]
    [ApiController]
    public class DependentResidentController : Controller
    {
        private readonly AppDBContext _context; 

        public DependentResidentController(AppDBContext context )
        {
            _context = context; 
        }

        // PUT: api/products/5
        [HttpPut("{UpdateDependentResident}")]
        public async Task<IActionResult> UpdateDependentResident(int id, [FromBody] DependentResident updatedDependentResident)
        {
            if (id != updatedDependentResident.ID)
            {
                return BadRequest("DependentResident ID mismatch.");
            }

            //var existingDependentResident = await _service.UpdateDependentResidentAsync(updatedDependentResident.ID, updatedDependentResident);
            var existingDependentResident = await _context.PrimaryResidents.FindAsync(updatedDependentResident.ID);

            if (existingDependentResident == null)
            {
                return NotFound();
            }

            _context.Entry(existingDependentResident).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingDependentResident);
        }


        [HttpPost("AddDependentResident")]
        public async Task<IActionResult> AddDependentResident([FromBody] DependentResident obj)
        {
            var existingDependentResident = await _context.DependentResidents.FindAsync(0);
            if (existingDependentResident != null)
            {
                return BadRequest("DependentResident Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllDependentResidents()
        {
            var DependentResidents = await _context.DependentResidents.ToListAsync();
            return Ok(DependentResidents);
        }


        [HttpPost("{AddDependentResidents}")]
        public async Task<IActionResult> AddDependentResidents([FromBody] List<DependentResident> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

            using var transaction = await _context.Database.BeginTransactionAsync();
            // Turn IDENTITY_INSERT ON
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT DependentResident ON");

            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.DependentResidents
                        .FirstOrDefaultAsync(p => p.ID == objID.ID);

                    if (existingobj != null)
                    {
                        //  var existingdependentResident = await _service.UpdateDependentResidentAsync(objID.ID, existingobj);
                        var existingdependentResident = await _context.DependentResidents.FindAsync(0);
                        if (existingdependentResident == null)
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        _context.DependentResidents.Add(objID);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                // Turn IDENTITY_INSERT OFF
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT DependentResident  OFF");

                return Ok(new { message = $"{Obj.Count} DependentResident processed successfully" });
            }
            catch (Exception ex)
            {
                   await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }





}
