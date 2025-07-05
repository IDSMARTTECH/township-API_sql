using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Township_API.Models.commonTypes;
using Township_API.Models;
using Township_API.Data;
using Microsoft.AspNetCore.Authorization;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ANPRController : Controller
    {
        private readonly AppDBContext _context; 
        public ANPRController(AppDBContext context)
        {
            _context = context;
         
        }
        [Authorize]
        [HttpPost("ANPRMessage")]
        public async Task<IActionResult> ANPRMessage([FromBody] ANPR obj)
        {
            //if (!ModelState.IsValid)
            //{
            //    var errors = ModelState
            //        .Where(ms => ms.Value.Errors.Any())
            //        .ToDictionary(
            //            kvp => kvp.Key,
            //            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            //        );

            //    return BadRequest(new { message = "Validation Failed", errors });
            //}

            var existingANPR = await _context.ANPRs.FindAsync(0);
            if (existingANPR != null)
            {
                return BadRequest("Anpr Message Entry Exists.");
            }
            var existsOBJ = await _context.ANPRs.Where(p => p.number_plate == obj.number_plate && p.camera_id == obj.camera_id && p.timestamp == obj.timestamp).ToListAsync();
            if (existsOBJ != null)
            {
                if (existsOBJ.Count > 0)
                    return BadRequest("number_plate Name with this Agency is already Exists.");
            }


            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Contractor ON");
            _context.Add(obj);
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Contractor OFF");

            await _context.SaveChangesAsync();
                       
            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllAnprMessages()
        {
            try
            {
                var message = await _context.ANPRs.OrderByDescending(p => p.messageid).ToListAsync();
                //if (message != null)
                //{
                //    if (message.Count > 0)
                //    {
                //        foreach (var res in message)
                //        {
                //            int mdl = (int)ModuleTypes.ContractorType;
                //            var nr = await _context.ModuleData.Where(u => u.ID.ToString() == res.ContactorType.ToString() && u.ModuleType.ToString() == mdl.ToString()).FirstOrDefaultAsync();
                //            if (nr != null)
                //            {
                //                res.ContractorTypeName = nr.Name.ToString();
                //            }
                //        }
                //    }
                //}
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest("Error :" + ex.Message.ToString());
            }
        }

        //[HttpGet("GetContractor/{id}")]
        //public async Task<IActionResult> GetContractor(int id)
        //{
        //    try
        //    {
        //        var Contractors = await _context.Contractors.Where(p => p.ID == id && p.LogicalDeleted == 0).OrderByDescending(p => p.ID).ToListAsync();

        //        if (Contractors != null)
        //        {
        //            if (Contractors.Count > 0)
        //            {
        //                foreach (var res in Contractors)
        //                {
        //                    int mdl = (int)ModuleTypes.ContractorType;
        //                    var nr = await _context.ModuleData.Where(u => u.ID.ToString() == res.ContactorType.ToString() && u.ModuleType.ToString() == mdl.ToString()).FirstOrDefaultAsync();
        //                    if (nr != null)
        //                    {
        //                        res.ContractorTypeName = nr.Name.ToString();
        //                    }
        //                }
        //            }
        //        }
        //        return Ok(Contractors);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error :" + ex.Message.ToString());
        //    }
        //}


    }
}
