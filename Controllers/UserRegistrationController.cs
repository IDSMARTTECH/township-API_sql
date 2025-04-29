using Microsoft.AspNetCore.Mvc;
using Township_API.Models;
using Township_API.Services;
using Township_API.Data;
using Microsoft.EntityFrameworkCore;

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
        [HttpPut("{UpdateUser}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            var existingUser = await _context.UserRegisters.FindAsync(id);
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


        [HttpGet("CardAccessRights\\{ID}")]
        public async Task<IActionResult> GetCardAccessRights(string IdNumber)
        {
            if (IdNumber != null)
            {
                try
                {
                    var jsonWrapper = new DependentJsonWrapper
                    {
                        UserNRDAccess = _context._userNRDAccess.Where(p => p.CardHolderID != null && p.CardHolderID.ToString() == IdNumber).ToList(),
                        UserBuildingAccess = _context._userBuildingAccess.Where(p => p.CardHolderID != null && p.CardHolderID.ToString() == IdNumber).ToList(),
                        UserAminitiesAccess = _context._userAmenitiesAccess.Where(p => p.CardHolderID != null && p.CardHolderID.ToString() == IdNumber).ToList()
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
                    var existingobj = await _context._userDoorAccess.Where(p => p.CardHolderID == objID.CardHolderID && p.ModuleID == objID.ModuleID)
                        .FirstOrDefaultAsync();

                    if (existingobj != null)
                    {
                        var existingDtls = await _context._userDoorAccess.FindAsync(objID.ID);
                        if (existingDtls == null)
                        {
                            return NotFound();
                        }
                        existingDtls.ModuleID = objID.ModuleID;
                        existingDtls.CardHolderID = objID.CardHolderID;
                        existingDtls.sun = objID.sun;
                        existingDtls.mon = objID.mon;
                        existingDtls.tus = objID.tus;
                        existingDtls.wed = objID.wed;
                        existingDtls.thu = objID.thu;
                        existingDtls.fri = objID.fri;
                        existingDtls.sat = objID.sat;
                        existingDtls.validTillDate = objID.validTillDate;
                        existingDtls.isactive = objID.isactive;   
                        existingDtls.updatedby = objID.updatedby;
                        existingDtls.updatedon = objID.updatedon; 
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context._userDoorAccess.Add(objID);
                        await _context.SaveChangesAsync();
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
