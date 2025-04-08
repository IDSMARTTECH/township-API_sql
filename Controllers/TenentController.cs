﻿using Microsoft.AspNetCore.Mvc;
using Township_API.Models;
using Township_API.Services;
using Township_API.Data;
using Microsoft.EntityFrameworkCore;
using Township_API.Models;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenentController : Controller
    {
        private readonly AppDBContext _context; 
        
        public TenentController(AppDBContext context )
        {
            _context = context; 
        }
 

        // PUT: api/products/5
        [HttpPut("{UpdateTenent}")]
        public async Task<IActionResult> UpdateTenent(int id, [FromBody] PrimaryTenent updatedTenent)
        {
            if (id != updatedTenent.ID)
            {
                return BadRequest("Tenent ID mismatch.");
            }

            //var existingTenent = await _service.UpdatePrimaryTenentAsync(updatedTenent.ID, updatedTenent);
            var existingTenent = await _context.PrimaryTenents.FindAsync(updatedTenent.ID); 
            if (existingTenent == null)
            {
                return NotFound();
            }

            _context.Entry(existingTenent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingTenent);
        }


        [HttpPost("AddTenent")]
        public async Task<IActionResult> AddTenent([FromBody] PrimaryTenent obj)
        {
            var existingTenent = await _context.PrimaryTenents.FindAsync(0);
            if (existingTenent != null)
            {
                return BadRequest("Tenent Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllTenents()
        {
            var Tenents = await _context.PrimaryTenents.ToListAsync();
            return Ok(Tenents);
        }


        [HttpPost("{AddTenents}")]
        public async Task<IActionResult> AddTenents([FromBody] List<PrimaryTenent> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

            //using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.PrimaryTenents
                        .FirstOrDefaultAsync(p => p.ID == objID.ID);

                    if (existingobj != null)
                    {
                        //var existingTenent = await _service.UpdatePrimaryTenentAsync(objID.ID, existingobj);
                        var existingTenent = await _context.PrimaryTenents.FindAsync(objID.ID); 
                        if (existingTenent == null)
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        _context.PrimaryTenents.Add(objID);
                    }
                }

                await _context.SaveChangesAsync();
                //await transaction.CommitAsync();

                return Ok(new { message = $"{Obj.Count} Tenent processed successfully" });
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
    public class DependentTenentController : Controller
    {
        private readonly AppDBContext _context;  
        public DependentTenentController(AppDBContext context )
        {
            _context = context; 
        }
      

        // PUT: api/products/5
        [HttpPut("{UpdateDependentTenent}")]
        public async Task<IActionResult> UpdateDependentTenent(int id, [FromBody] DependentTenent updatedDTenent)
        {
            if (id != updatedDTenent.ID)
            {
                return BadRequest("Tenent ID mismatch.");
            }

            //var existingDependentTenent = await _service.UpdateDependentTenentAsync(id, updatedDTenent);
            var existingDependentTenent = await _context.DependentTenents.FindAsync(id);
            if (existingDependentTenent == null)
            {
                return NotFound();
            }

            _context.Entry(existingDependentTenent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingDependentTenent);
        }


        [HttpPost("AddDependentTenent")]
        public async Task<IActionResult> AddDependentTenent([FromBody]  DependentTenent obj)
        {
            var existingDependentTenent = await _context.DependentTenents.FindAsync(0);
            if (existingDependentTenent != null)
            {
                return BadRequest("DependentTenent Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllDependentTenents()
        {
            var DependentTenents = await _context.DependentTenents.ToListAsync();
            return Ok(DependentTenents);
        }



        [HttpPost("{AddDependentTenents}")]
        public async Task<IActionResult> AddTenents([FromBody] List<DependentTenent> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

           // using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.DependentTenents
                        .FirstOrDefaultAsync(p => p.ID == objID.ID);

                    if (existingobj != null)
                    {
                        //var existingTenent = await _service.UpdateDependentTenentAsync(objID.ID, existingobj);
                        var existingTenent = await _context.DependentTenents.FindAsync(objID.ID);
                        if (existingTenent == null)
                        {
                            return NotFound(); 
                        }
                    }
                    else
                    {
                        _context.DependentTenents.Add(objID);
                    }
                }

                await _context.SaveChangesAsync();
              //  await transaction.CommitAsync();

                return Ok(new { message = $"{Obj.Count} DependentTenent processed successfully" });
            }
            catch (Exception ex)
            {
              //  await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        } 


    }

}
