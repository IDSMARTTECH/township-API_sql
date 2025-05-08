using Microsoft.AspNetCore.Mvc;
using Township_API.Models;
using Township_API.Data;
using Microsoft.EntityFrameworkCore;
using Township_API.Services;
using static Township_API.Models.commonTypes;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : Controller
    {
        private readonly AppDBContext _context;
        public ContractorController(AppDBContext context)
        {
            _context = context;
        }


        // PUT: api/products/5
        [HttpPost("{UpdateContractor}/{id}")]
        public async Task<IActionResult> UpdateContractor(int id, [FromBody] Contractor updatedContractor)
        {
            if (id != updatedContractor.ID)
            {
                return BadRequest("Contractor ID mismatch.");
            }

            //var existingContractor = await _service.UpdateDependentLandownerAsync(updatedContractor.ID, updatedContractor);
            var existingContractor = await _context.Contractors.FindAsync(updatedContractor.ID);
            if (existingContractor == null)
            {
                return NotFound();
            }
            existingContractor.CSN = updatedContractor.CSN;
            existingContractor.IDNumber = updatedContractor.IDNumber;
            existingContractor.TagNumber = updatedContractor.TagNumber;
            existingContractor.PANnumber = updatedContractor.PANnumber;
            existingContractor.PassportNo = updatedContractor.PassportNo;
            existingContractor.LicenseNo = updatedContractor.LicenseNo;
            existingContractor.ICEno = updatedContractor.ICEno;
            existingContractor.AadharCardId = updatedContractor.AadharCardId;
            existingContractor.VoterID = updatedContractor.VoterID;
            existingContractor.FirstName = updatedContractor.FirstName;
            existingContractor.MiddletName = updatedContractor.MiddletName;
            existingContractor.LastName = updatedContractor.LastName;
            existingContractor.ShortName = updatedContractor.ShortName;
            existingContractor.Gender = updatedContractor.Gender;
            existingContractor.BloodGroup = updatedContractor.BloodGroup;
            existingContractor.DOB = updatedContractor.DOB;
            existingContractor.EmailID = updatedContractor.EmailID;
            existingContractor.MobileNo = updatedContractor.MobileNo;
            existingContractor.LandLine = updatedContractor.LandLine;
            existingContractor.CardIssueDate = updatedContractor.CardIssueDate;
            existingContractor.CardPrintingDate = updatedContractor.CardPrintingDate;
            existingContractor.RegistrationIssueDate = updatedContractor.RegistrationIssueDate;
            existingContractor.LogicalDeleted = updatedContractor.LogicalDeleted;
            _context.Entry(existingContractor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(existingContractor);
        }

        [HttpPost("AddContractor")]
        public async Task<IActionResult> AddContractor([FromBody] Contractor obj)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new { message = "Validation Failed", errors });
            }

            var existingContractor = await _context.ModuleData.FindAsync(0);
            if (existingContractor != null)
            {
                return BadRequest("Contractor Exists.");
            }

            await _context.SaveChangesAsync();
            int number = (int)AccessCardHilders.contractor;
            obj.IDNumber = number.ToString() + obj.ID.ToString("D10");

            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllContractors()
        {
            try
            {
                var Contractors = await _context.Contractors.OrderByDescending(p => p.ID).ToListAsync();
                return Ok(Contractors);
            }
            catch (Exception ex)
            {
                return BadRequest("Error :" + ex.Message.ToString());
            }
        }

        [HttpPost("{AddContractors}")]
        public async Task<IActionResult> AddContractors([FromBody] List<Contractor> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

            using var transaction = await _context.Database.BeginTransactionAsync();

            // Turn IDENTITY_INSERT ON
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Contractor ON");


            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.Contractors
                        .FirstOrDefaultAsync(p => p.ID == objID.ID);

                    if (existingobj != null)
                    {
                        //var existingContractor = await _service.UpdateContractorAsync(objID.ID, existingobj);
                        var existingContractor = await _context.Contractors.FindAsync(objID.ID);
                        if (existingContractor == null)
                        {
                            return NotFound();
                        }
                        existingContractor.CSN = objID.CSN;
                        existingContractor.IDNumber = objID.IDNumber;
                        existingContractor.TagNumber = objID.TagNumber;
                        existingContractor.PANnumber = objID.PANnumber;
                        existingContractor.PassportNo = objID.PassportNo;
                        existingContractor.LicenseNo = objID.LicenseNo;
                        existingContractor.ICEno = objID.ICEno;
                        existingContractor.AadharCardId = objID.AadharCardId;
                        existingContractor.VoterID = objID.VoterID;
                        existingContractor.FirstName = objID.FirstName;
                        existingContractor.MiddletName = objID.MiddletName;
                        existingContractor.LastName = objID.LastName;
                        existingContractor.ShortName = objID.ShortName;
                        existingContractor.Gender = objID.Gender;
                        existingContractor.BloodGroup = objID.BloodGroup;
                        existingContractor.DOB = objID.DOB;
                        existingContractor.EmailID = objID.EmailID;
                        existingContractor.MobileNo = objID.MobileNo;
                        existingContractor.LandLine = objID.LandLine;
                        existingContractor.CardIssueDate = objID.CardIssueDate;
                        existingContractor.CardPrintingDate = objID.CardPrintingDate;
                        existingContractor.RegistrationIssueDate = objID.RegistrationIssueDate;
                        existingContractor.LogicalDeleted = objID.LogicalDeleted;
                        _context.Entry(existingContractor).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Contractors.Add(objID);
                        await _context.SaveChangesAsync();
                    }
                }

                await transaction.CommitAsync();
                // Turn IDENTITY_INSERT OFF
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Contractor  OFF");

                return Ok(new { message = $"{Obj.Count} Contractor processed successfully" });
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
    public class DependentContractorController : Controller
    {
        private readonly AppDBContext _context;

        public DependentContractorController(AppDBContext context)
        {
            _context = context;
        }

        // PUT: api/products/5
        [HttpPost("{UpdateDependentContractor}/{id}")]
        public async Task<IActionResult> UpdateDependentContractor(int id, [FromBody] DependentContractor updatedDependentContractor)
        {
            if (id != updatedDependentContractor.ID)
            {
                return BadRequest("DependentContractor ID mismatch.");
            }

            //var existingDependentContractor = await _service.UpdateDependentContractorAsync(updatedDependentContractor.ID, updatedDependentContractor);
            var existingDependentContractor = await _context.PrimaryResidents.FindAsync(updatedDependentContractor.ID);

            if (existingDependentContractor == null)
            {
                return NotFound();
            }

            _context.Entry(existingDependentContractor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingDependentContractor);
        }


        [HttpPost("AddDependentContractor")]
        public async Task<IActionResult> AddDependentContractor([FromBody] DependentContractor obj)
        {
            var existingDependentContractor = await _context.DependentContractors.FindAsync(0);
            if (existingDependentContractor != null)
            {
                return BadRequest("DependentContractor Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllDependentContractors()
        {
            var DependentContractors = await _context.DependentContractors.OrderByDescending(p => p.ID).ToListAsync();
            return Ok(DependentContractors);
        }


        [HttpPost("AddDependentContractors")]
        public async Task<IActionResult> AddDependentContractors([FromBody] List<DependentContractor> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

            using var transaction = await _context.Database.BeginTransactionAsync();
            // Turn IDENTITY_INSERT ON
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT DependentContractor ON");

            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.DependentContractors
                        .FirstOrDefaultAsync(p => p.ID == objID.ID);

                    if (existingobj != null)
                    {
                        //  var existingDependentContractor = await _service.UpdateDependentContractorAsync(objID.ID, existingobj);
                        var existingDependentContractor = await _context.DependentContractors.FindAsync(0);
                        if (existingDependentContractor == null)
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        _context.DependentContractors.Add(objID);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                // Turn IDENTITY_INSERT OFF
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT DependentContractor  OFF");

                return Ok(new { message = $"{Obj.Count} DependentContractor processed successfully" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }

}
