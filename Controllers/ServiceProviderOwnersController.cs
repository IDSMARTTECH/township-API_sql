using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Township_API.Data;
using Township_API.Models;

namespace Township_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderOwnersController : Controller
    {

        private readonly AppDBContext _context;
        public ServiceProviderOwnersController(AppDBContext context)
        {
            _context = context;
        }



        [HttpPost("AddUPdateServiceProviderOwners")]
        public async Task<IActionResult> AddUPdateServiceProviderOwners([FromBody] ServiceProviderOwners obj)
        {
            try
            {

                var ExistsSproviders = await _context.ServiceProviderOwners.Where(p => p.Id != obj.Id && p.SProviderID == obj.SProviderID && p.Hid == obj.Hid).ToListAsync();
                if (ExistsSproviders != null)
                {
                    if (ExistsSproviders.Count > 0)
                        return BadRequest("ServiceProvider with this owner allready Exists.");
                }
                var ExistsSprovider = await _context.ServiceProviderOwners.Where(p => p.SProviderID == obj.SProviderID && p.Hid == obj.Hid).FirstOrDefaultAsync();

                if (ExistsSprovider != null)
                {
                    if (ExistsSprovider.Id > 0)
                    {
                        ExistsSprovider.SProviderID = obj.SProviderID;
                        ExistsSprovider.Hid = obj.Hid;
                        ExistsSprovider.OwnerDetails = obj.OwnerDetails;
                        ExistsSprovider.isActive = obj.isActive;


                        _context.Entry(ExistsSprovider).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return Ok(ExistsSprovider);

                    }

                }
                if (obj != null)
                {
                    _context.Add(obj);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = $"{obj.Id}  Owner added successfully" });
                }
                return BadRequest("unable to save data!");
            }
            catch (Exception ex)
            {
                return BadRequest("Data Error : " + ex.Message.ToString().ToString());
            }
        }


        // GET: api/products 
        [HttpGet("GetAllServiceProviderOwners/{ServiceProviderId}")]
        public async Task<IActionResult> GetAllServiceProviderOwners(string ServiceProviderId)
        {
            try
            {
                var Sproviders = await _context.ServiceProviderOwners.Where(p=>p.SProviderID==ServiceProviderId) .OrderByDescending(p => p.Id).ToListAsync();

                //OwnerName building Neighbourhood FlatNumber
                foreach (var res in Sproviders)
                {
                    var nr = await _context.AllCardHolders.Where(u => u.IDNumber.ToString() == res.Hid.ToString()).FirstOrDefaultAsync();
                    if (nr != null)
                    {
                        res.OwnerName = nr.shortname.ToString();
                        res.building = nr.NRD.ToString();
                        res.Neighbourhood = nr.Bld.ToString();
                        res.FlatNumber = nr.FlatNumber.ToString();
                    }
                }
                return Ok(Sproviders);
            }
            catch (Exception ex)
            {
                return BadRequest("Data Error : " + ex.Message.ToString().ToString());
            }
        }


    }
}
