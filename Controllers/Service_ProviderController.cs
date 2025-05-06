using Microsoft.AspNetCore.Mvc;
using Township_API.Data;
using Township_API.Services;
using Township_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Numerics;
using static Township_API.Models.commonTypes;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Service_ProviderController : Controller
    {
        private readonly AppDBContext _context;
        public Service_ProviderController(AppDBContext context)
        {
            _context = context;
        }

        // PUT: api/products/5
        [HttpPost("{UpdateServiceProvider}/{id}")]
        public async Task<IActionResult> UpdateServiceProvider(int id, [FromBody] Service_Provider updatedServiceProvider)
        {
            try
            {
                if (id != updatedServiceProvider.ID)
                {
                    return BadRequest("ServiceProvider ID mismatch.");
                }

                //var existingServiceProvider = await _service.UpdateServiceProviderAsync(updatedServiceProvider.ID, updatedServiceProvider);
                var existingServiceProvider = await _context.ServiceProviders.FindAsync(updatedServiceProvider.ID);

                if (existingServiceProvider == null)
                {
                    return NotFound();
                }
                existingServiceProvider.ID = updatedServiceProvider.ID;
                existingServiceProvider.code = updatedServiceProvider.code;
                existingServiceProvider.name = updatedServiceProvider.name;
                existingServiceProvider.email = updatedServiceProvider.email;
                existingServiceProvider.phone = updatedServiceProvider.phone;
                existingServiceProvider.ServiceProviderID = updatedServiceProvider.ServiceProviderID;
                existingServiceProvider.role = updatedServiceProvider.role;
                existingServiceProvider.isactive = updatedServiceProvider.isactive; 
                existingServiceProvider.updatedby = updatedServiceProvider.updatedby;
                existingServiceProvider.updatedon = updatedServiceProvider.updatedon;
                _context.Entry(existingServiceProvider).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(existingServiceProvider);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPost("AddServiceProvider")]
        public async Task<IActionResult> AddServiceProvider([FromBody] Service_Provider obj)
        {
            try
            {
                var existingServiceProvider = await _context.ServiceProviders.FindAsync(0);
                if (existingServiceProvider != null)
                {
                    return BadRequest("ServiceProvider Exists.");
                }

                _context.Add(obj);
                await _context.SaveChangesAsync();
                int number = (int)AccessCardHilders.ServiceProvider;
                obj.code = number.ToString() + obj.ID.ToString("D10");

                await _context.SaveChangesAsync();
                return Ok(existingServiceProvider);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        // GET: api/products 
        [HttpGet("ServiceProvider")]
        public async Task<IActionResult> GetAllServiceProviders()
        { 
            var ServiceProviders = await _context.ServiceProviders.OrderByDescending(p =>p.ID).ToListAsync();
            return Ok(ServiceProviders);
        }

        // GET: api/products 
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetService_providerDetails(int ID)
        {
            var sProviders = await _context.ServiceProviders.Where(p => p.ID == ID).ToListAsync();
            string? IdNumber = sProviders[0].code;
            if (IdNumber != null)
            {
                try
                {
                    var jsonWrapper = new DependentJsonWrapper
                    {
                        Owners = sProviders,
                        DependentOwners = null,
                        Vehicles = _context.Vehicles.Where(p => p.TagUID == IdNumber).ToList(),
                        UserAllAccess = await _context._userAllAccess.Where(p => p.CardHolderID == IdNumber).ToListAsync(),
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
            return Ok(new { message = $"Contractor records not found!" });
        }


        [HttpPost("AddServiceProviders")]
        public async Task<IActionResult> AddServiceProviders([FromBody] List<Service_Provider> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

            using var transaction = await _context.Database.BeginTransactionAsync();
            // Turn IDENTITY_INSERT ON
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblServiceProvider ON");

            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.ServiceProviders
                        .FirstOrDefaultAsync(p => p.ID == objID.ID);

                    if (existingobj != null)
                    {
                        //  var existingServiceProvider = await _service.UpdateServiceProviderAsync(objID.ID, existingobj);
                        var existingServiceProvider = await _context.ServiceProviders.FindAsync(0);
                        if (existingServiceProvider == null)
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        _context.ServiceProviders.Add(objID);
                        await _context.SaveChangesAsync();
                        int number = (int)AccessCardHilders.ServiceProvider;
                        objID.code = number.ToString() + objID.ID.ToString("D10");
                        await _context.SaveChangesAsync();
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                // Turn IDENTITY_INSERT OFF
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblServiceProvider  OFF");

                return Ok(new { message = $"{Obj.Count} ServiceProvider processed successfully" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }

}
