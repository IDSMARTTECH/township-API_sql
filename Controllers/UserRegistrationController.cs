using Microsoft.AspNetCore.Mvc;
using Township_API.Models;
using Township_API.Services;
using Township_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : Controller
    {
        private readonly AppDBContext _context;

        public UserRegistrationController(AppDBContext context)
        {
            _context = context;
        }

        // PUT: api/products/5
        [HttpPost("{UpdateUser}/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            var existingUser = await _context.UserRegisters.FindAsync(0);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Update properties
            existingUser.uid = updatedUser.uid;
            existingUser.name = updatedUser.name;
            existingUser.email = updatedUser.email;
            existingUser.phone = updatedUser.phone;
            existingUser.password = updatedUser.password;
            existingUser.Role = updatedUser.Role;

            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User User)
        {
            var existingUser = await _context.UserRegisters.FindAsync(0);
            if (existingUser != null)
            {
                return BadRequest("User Exists.");
            }
            _context.Add(User);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var users = await _service.GetAllAsync();
            var users = await _context.UserRegisters.ToListAsync();
            return Ok(users);
        }


        [HttpGet("{IdNumber}")]
        public async Task<IActionResult> CardAccessRights(string IdNumber)
        {
            if (IdNumber != null)
            {
                try
                {
                    var jsonWrapper = new DependentJsonWrapper
                    {
                        UserAllAccess = await _context._userAllAccess.Where(p => p.CardHolderID == IdNumber).ToListAsync(),
                        UserNRDAccess = await _context._userNRDAccess.Where(p => p.CardHolderID == IdNumber).ToListAsync(),
                        UserBuildingAccess = await _context._userBuildingAccess.Where(p => p.CardHolderID == IdNumber).ToListAsync(),
                        UserAminitiesAccess = await _context._userAmenitiesAccess.Where(p => p.CardHolderID == IdNumber).ToListAsync()
                    };

                    return Ok(jsonWrapper);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message.ToString());
                }
            }
            return Ok(new { message = $"No data found!" });
        }



        [HttpPost("AddUpdateCardAccessRights")]
        public async Task<IActionResult> AddUpdateCardAccessRightsDetails([FromBody] List<DoorAccess> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

            //using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var objID in Obj)
                {
                    DoorAccess? existingobj;
                    bool isEdit=false;
                    
                        existingobj = await _context._userDoorAccess.Where(p => p.CardHolderID == objID.CardHolderID && p.moduleID.ToString() == objID.moduleID.ToString())
                                 .FirstOrDefaultAsync();

                    if (existingobj != null) 
                        isEdit = true;
                    
                    if (isEdit == false && objID.id > 0)
                    {
                         existingobj = await _context._userDoorAccess.FindAsync(objID.id);
                        if (existingobj == null)
                        {
                           return BadRequest(new { message = $"Card Access Details mismatch found" });
                        }
                        isEdit = true;
                    }
                    if (!isEdit)
                    {
                        _context._userDoorAccess.Add(objID);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                       if (existingobj != null) { 
                        existingobj.moduleID = objID.moduleID;
                        existingobj.CardHolderID = objID.CardHolderID;
                        existingobj.sun = objID.sun;
                        existingobj.mon = objID.mon;
                        existingobj.tus = objID.tus;
                        existingobj.wed = objID.wed;
                        existingobj.thu = objID.thu;
                        existingobj.fri = objID.fri;
                        existingobj.sat = objID.sat;
                        existingobj.validTillDate = objID.validTillDate;
                        existingobj.isactive = objID.isactive;
                        existingobj.updatedby = objID.updatedby;
                        existingobj.updatedon = objID.updatedon;
                        await _context.SaveChangesAsync();
                        }
                    } 
                }

                //   await transaction.CommitAsync();

                return Ok(new { message = $"Card Access Details processed successfully" });
            }
            catch (Exception ex)
            {
                //   await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }

        }


    }
}
