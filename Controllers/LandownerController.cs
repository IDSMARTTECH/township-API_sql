
using Microsoft.AspNetCore.Mvc;
using Township_API.Models;
using Township_API.Services;
using Township_API.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandownerController : Controller
    {
        private readonly AppDBContext _context;  
        public LandownerController(AppDBContext context)
        {
            _context = context; 
        } 
        
        // PUT: api/products/5
        [HttpPut("{UpdateLandowner}")]
        public async Task<IActionResult> UpdateLandowner(int id, [FromBody] PrimaryLandowner updatedLandowner)
        {
            if (id != updatedLandowner.ID)
            {
                return BadRequest("Landowner ID mismatch.");
            }

            var existingLandowner = await _context.Landowners.FindAsync(id);
            //var existingLandowner = await _service.UpdateLandownerAsync(updatedLandowner.ID, updatedLandowner);
            if (existingLandowner == null)
            {
                return NotFound();
            }

            _context.Entry(existingLandowner).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingLandowner);
        }
         
        [HttpPost("AddLandowner")]
        public async Task<IActionResult> AddLandowner([FromBody] PrimaryLandowner obj)
        {
            try
            {
                var existingLandowner = await _context.Landowners.FindAsync(0);
                if (existingLandowner != null)
                {
                    return BadRequest("Landowner Exists.");
                }
                _context.Add(obj);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Error: " + ex.Message.ToString());
                throw;
            }
        }
         

        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllLandowners()
        {
            var Landowners = await _context.Landowners.ToListAsync();
            return Ok(Landowners);
        }

        [HttpPost("{AddLandOwners}")]
        public async Task<IActionResult> AddLandOwners([FromBody] List<PrimaryLandowner> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

            //using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.Landowners
                        .FirstOrDefaultAsync(p => p.ID == objID.ID);

                    if (existingobj != null)
                    {
                        // var existingLandOwner = await _service.UpdateLandownerAsync(objID.ID, existingobj);
                        var existingLandOwner = await _context.Landowners.FindAsync(objID.ID);
                        if (existingLandOwner == null)
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        _context.Landowners.Add(objID);
                    }
                }

                await _context.SaveChangesAsync();
             //   await transaction.CommitAsync();

                return Ok(new { message = $"{Obj.Count} Landowners processed successfully" });
            }
            catch (Exception ex)
            {
             //   await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
    
        }
  


    }


    [Route("api/[controller]")]
    [ApiController]
    public class DependentLandOwnerController : Controller
    {
        private readonly AppDBContext _context; 
         
        public DependentLandOwnerController(AppDBContext context)
        {
            _context = context; 
        }
 

        // PUT: api/products/5
        [HttpPut("{UpdateDependentLandOwner}")]
        public async Task<IActionResult> UpdateDependentLandOwner(int id, [FromBody]  DependentLandOwner updatedDLandOwner)
        {
            if (id != updatedDLandOwner.ID)
            {
                return BadRequest("DependentLandOwner ID mismatch.");
            }

            //var existingDependentLandOwner = await _service.UpdateDependentLandownerAsync(updatedDLandOwner.ID, updatedDLandOwner);
            var existingDependentLandOwner = await _context.DependentLandowners.FindAsync(updatedDLandOwner.ID); 
            if (existingDependentLandOwner == null)
            {
                return NotFound();
            }

            _context.Entry(existingDependentLandOwner).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingDependentLandOwner);
        }

        [HttpPost("{AddDependentLandOwner}")]
        public async Task<IActionResult> AddDependentLandOwner([FromBody]  DependentLandOwner obj)
        {
            var existingDependentLandOwner = await _context.DependentLandowners.FindAsync(0);
            if (existingDependentLandOwner != null)
            {
                return BadRequest("Dependent LandOwner Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }
         
        [HttpGet]
        public async Task<IActionResult> GetAllDependentLandowners()
        {
            var DependentLandOwners = await _context.DependentLandowners.ToListAsync();
            return Ok(DependentLandOwners);
        }

        [HttpPost("AddDependentLandOwners")]
        public async Task<IActionResult> AddDependentLandOwners([FromBody] List<DependentLandOwner> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

          //  using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.DependentLandowners
                        .FirstOrDefaultAsync(p => p.ID == objID.ID);

                    if (existingobj != null)
                    { 
                        //var existingDependentLandOwner = await _service.UpdateDependentLandownerAsync(objID.ID, objID);
                        var existingDependentLandOwner = await _context.DependentLandowners.FindAsync(objID.ID); 
                        if (existingDependentLandOwner == null)
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        _context.DependentLandowners.Add(objID);
                    }
                }

                await _context.SaveChangesAsync();
                //await transaction.CommitAsync();

                return Ok(new { message = $"{Obj.Count} Dependent Landowners processed successfully" });
            }
            catch (Exception ex)
            {
              //  await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }
        
    }
     
}
