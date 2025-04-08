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

        public UserRegistrationController(AppDBContext context )
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
            existingUser.UID  = updatedUser.UID;
            existingUser.UserName = updatedUser.UserName;
            existingUser.Email = updatedUser.Email;
            existingUser.Phone = updatedUser.Phone;
            existingUser.Password = updatedUser.Password;
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

    }
}
