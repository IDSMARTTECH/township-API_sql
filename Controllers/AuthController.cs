
using Microsoft.AspNetCore.Mvc;
using Township_API.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Township_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _context;

        public AuthController( IConfiguration configuration, AppDBContext context)
        {
            //UserManager<IdentityUser> userManager, SignInManager< IdentityUser > signInManager,
            //_userManager = userManager;
            //_signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _context.UserRegisters.Where(p => (p.uid == model.Email || p.email == model.Email) && p.password == model.Password).ToListAsync();
            if (user != null)
            {
                if (user.Count > 0)
                {
                    var role = user[0].Role;
                  //  var roles = await _context.Roles.Where(p => p.RoleID == user[0].roleID).FirstOrDefault();
                    // var token = GenerateJwtToken(user, roles);
                    return Ok(user);
                }
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }


}
