
using Microsoft.AspNetCore.Mvc;
using Township_API.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Township_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using Image = Township_API.Models.Image;
using System.ComponentModel;


namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _context;

        public AuthController(IConfiguration configuration, AppDBContext context)
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

        [HttpGet("CardHolderSearch/{search}")]
        public async Task<IActionResult> CardHolderSearch(string? search=null)
        {
            List<AllCardHolder> cardHolder = new List<AllCardHolder>();

            try
            {
//item.Column.Contains("value")
                if (search == null)
                    search = "";
                if (search.Length >2) {
                        cardHolder = await _context.AllCardHolders.Where(p => (p.IDNumber.Contains(search))|| (p.shortname.Contains(search))).ToListAsync();
                    if (cardHolder != null)
                    {
                        if (cardHolder.Count > 0)
                        {
                            return Ok(cardHolder);
                        }

                    }
                         
                }       }
            catch (Exception ex)
            {

            }

            return Ok(cardHolder);

        }


        //    [HttpGet("{imageName}")]
        //    public IActionResult GetImage(string imageName)
        //    {
        //        try
        //        {
        //            // Path to your image file
        //            var imagePath = Path.Combine("Images", imageName);

        //            if (!System.IO.File.Exists(imagePath))
        //            {
        //                return NotFound();
        //            }

        //            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
        //            var contentType = GetContentType(imagePath); // Determine MIME type

        //            return File(imageBytes, contentType)

        //}
        //    catch (Exception ex)
        //    {
        //        var r = ex.Message.ToString();
        //    }

        //}
        //public async Task<Image?> GetImageByIdAsync(string filenm)
        //{
        //    try
        //    {
        //        var img = (Image)_context.Images.Where(p => p.FileName == filenm);
        //        return img;

        //    }

        //    catch (Exception ex)
        //    {
        //        var r = ex.Message.ToString();
        //    }
        //    return null;

        //}
        [HttpGet("{imageName}")]
        public IActionResult GetImageStream(string imageName)
        {
            var imagePath = Path.Combine("Images", imageName);

            if (!System.IO.File.Exists(imagePath))
                return NotFound();

            var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            var contentType = GetContentType(imagePath);

            return File(stream, contentType);
        }

        [HttpGet("{filenm}")]
        public async Task<IActionResult> GetImage(string filenm)
        {
            //var image = await GetImageByIdAsync(filenm);
            var image = (Image)_context.Images.Where(p => p.FileName == filenm).FirstOrDefault();            
            if (image == null)
                return NotFound();

            return File(image.Data, image.ContentType); // Serves binary as proper image
        }
                
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("File is empty");

                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                var imageData = ms.ToArray();

                var imageEntity = new Image
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Data = imageData
                };

                _context.Images.Add(imageEntity);
                await _context.SaveChangesAsync();

                return Ok(new { imageEntity.ImageId });
            }
            catch (Exception ex)
            {
                var r = ex.Message.ToString();
                return BadRequest("error: "+r.ToString());
            }
           
        }

        private string GetContentType(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                _ => "application/octet-stream",
            };
        }
    }

    /* 
     //private string UpdateCardHolderImage(int user,  )
     //{


     //}

     private BitmapImage LoadImageFromDatabase(int imageId)
     {
         byte[] imageBytes = null;

         using (SqlConnection conn = new SqlConnection("Your_Connection_String"))
         {
             conn.Open();
             using (SqlCommand cmd = new SqlCommand("SELECT ImageData FROM Images WHERE Id = @Id", conn))
             {
                 cmd.Parameters.AddWithValue("@Id", imageId);
                 using (SqlDataReader reader = cmd.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                         imageBytes = (byte[])reader["ImageData"];
                     }
                 }
             }
         }

         if (imageBytes != null)
         {
             using (MemoryStream ms = new MemoryStream(imageBytes))
             {
                 BitmapImage bitmap = new BitmapImage();
                 bitmap.BeginInit();
                 bitmap.CacheOption = BitmapCacheOption.OnLoad;
                 bitmap.StreamSource = ms;
                 bitmap.EndInit();
                 return bitmap;
             }
         }

         return null;
     }

     private BitmapImage LoadImageFromDatabase(int imageId)
     {
         byte[] imageBytes = null;

         using (SqlConnection conn = new SqlConnection(connectionString))
         {
             conn.Open();
             using (SqlCommand cmd = new SqlCommand("SELECT ImageData FROM Images WHERE Id = @Id", conn))
             {
                 cmd.Parameters.AddWithValue("@Id", imageId);
                 using (SqlDataReader reader = cmd.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                         imageBytes = (byte[])reader["ImageData"];

                     }
                 }
             }
         }
         if (imageBytes != null)
             return imageBytes;

         //if (imageBytes != null)
         //{
         //    using (MemoryStream ms = new MemoryStream(imageBytes))
         //    {
         //        BitmapImage bitmap = new BitmapImage();
         //        bitmap.BeginInit();
         //        bitmap.CacheOption = BitmapCacheOption.OnLoad;
         //        bitmap.StreamSource = ms;
         //        bitmap.EndInit();
         //        return bitmap;
         //    }
         //}

         return null;
     }
     */
}
    