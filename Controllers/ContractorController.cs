using Microsoft.AspNetCore.Mvc;
using Township_API.Models; 
using Township_API.Data;
using Microsoft.EntityFrameworkCore;
using Township_API.Services;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : Controller
    {
        private readonly AppDBContext _context; 
        public ContractorController(AppDBContext context )
        {
            _context = context;  
        }
         /*

        // PUT: api/products/5
        [HttpPut("{UpdateContractor}")]
        public async Task<IActionResult> UpdateContractor(int id, [FromBody]  Contractor updatedContractor)
        {
            if (id != updatedContractor.ID)
            {
                return BadRequest("Contractor ID mismatch.");
            }

            //var existingContractor = await _service.UpdateDependentLandownerAsync(updatedContractor.ID, updatedContractor);
            var existingContractor = await _context.ModuleData.FindAsync(updatedContractor.ID); 
            if (existingContractor == null)
            {
                return NotFound();
            }

            _context.Entry(existingContractor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingContractor);
        }


        [HttpPost("AddContractor")]
        public async Task<IActionResult> AddContractor([FromBody]  ContractorType obj)
        {
            var existingContractor = await _context.ModuleData.FindAsync(0);
            if (existingContractor != null)
            {
                return BadRequest("Contractor Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllContractors()
        {
            var Contractors = await _context.ModuleData.ToListAsync();
            return Ok(Contractors);
        }

        */

    }


    [Route("api/[controller]")]
    [ApiController]
    public class ContractorDependentLandOwnerController : Controller
    {
        private readonly AppDBContext _context;

        // private readonly iService _service;

        public ContractorDependentLandOwnerController(AppDBContext context)
        {
            _context = context;
        }
         


    } 
    
}
