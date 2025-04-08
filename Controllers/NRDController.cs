﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Township_API.Data;
using Township_API.Models;
using Township_API.Services;

namespace Township_API.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class NRDController : Controller
    {
        private readonly AppDBContext _context;
        //private readonly iService _service;
        public NRDController(AppDBContext context)//, iService srv
        {
            _context = context;
          //  _service = srv;
        }
         
        // PUT: api/products/5
        [HttpPut("{UpdateNRD}")]
        public async Task<IActionResult> UpdateNRD(int id, [FromBody] NRD updatedOBJ)
        {
            if (id != updatedOBJ.ID)
            {
                return BadRequest("NRD ID mismatch.");
            }

            var existingOBJ = await _context.ModuleData.FindAsync(id);
            if (existingOBJ == null)
            {
                return NotFound();
            }

            // Update properties
            existingOBJ.Name = updatedOBJ.Name;
            existingOBJ.Code = updatedOBJ.Code;
            existingOBJ.ParentID = updatedOBJ.ParentID;
            existingOBJ.isactive = updatedOBJ.isactive;

            _context.Entry(existingOBJ).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingOBJ);
        }


        [HttpPost("AddNRD")]
        public async Task<IActionResult> AddNRD([FromBody] NRD obj)
        {
            var existingOBJ = await _context.ModuleData.FindAsync(0);
            if (existingOBJ != null)
            {
                return BadRequest("NRD Exists.");
            }

            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 

        [HttpGet]
        public async Task<IActionResult> GetAllNRDs()
        {
            try
            {
                int objval = (int)commonTypes.ModuleTypes.NRD;
                var OBJs = await _context.ModuleData.Where(n => n.TypeID == objval && n.ID > objval).ToListAsync();
                
                return Ok(OBJs);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost("bulkSaveNRD")]
        public async Task<IActionResult> BulkSave([FromBody] List<NRD> Obj)
        {

            //using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.ModuleData
                        .FirstOrDefaultAsync(v => v.ID == updatedObj.ID);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.Code = updatedObj.Code;
                        existingOBJ.Name = updatedObj.Name;
                        existingOBJ.isactive = updatedObj.isactive;
                        existingOBJ.createdby = 0;
                        existingOBJ.updatedby = 0;
                        existingOBJ.createdon = DateTime.Now;
                    }
                    else
                    {
                        _context.ModuleData.Add(updatedObj);
                    }
                }

                await _context.SaveChangesAsync();
                //  await transaction.CommitAsync();

                return Ok(new { message = $"NRD processed successfully" });
            }
            catch (Exception ex)
            {
                //     await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }
        
    }



    [Route("api/[controller]")]
    [ApiController]
    public class PhaseController : Controller
    {
        private readonly AppDBContext _context;

        public PhaseController(AppDBContext context)
        {
            _context = context;
        }

        // PUT: api/products/5
        [HttpPut("{UpdatePhase}")]
        public async Task<IActionResult> UpdatePhase(int id, [FromBody] Phase updatedObj)
        {
            if (id != updatedObj.ID)
            {
                return BadRequest("{updatedObj.code} ID mismatch.");
            }

            var existingOBJ = await _context.ModuleData.FindAsync(id);
            if (existingOBJ == null)
            {
                return NotFound();
            }

            // Update properties
            existingOBJ.Name = updatedObj.Name;
            existingOBJ.Code = updatedObj.Code;
            existingOBJ.ParentID = updatedObj.ParentID;
            existingOBJ.isactive = updatedObj.isactive;


            _context.Entry(existingOBJ).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingOBJ);
        }


        [HttpPost("AddPhase")]
        public async Task<IActionResult> AddPhase([FromBody] Phase obj)
        {
            var existingPhase = await _context.ModuleData.FindAsync(0);
            if (existingPhase != null)
            {
                return BadRequest("Phase Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllPhases()
        {
            var Phases = await _context.ModuleData.Where(p => (p.TypeID == (int)commonTypes.ModuleTypes.Phases && p.ID > (int)commonTypes.ModuleTypes.Phases)).ToListAsync();
            return Ok(Phases);
        }

        [HttpPost("bulkSavePhase")]
        public async Task<IActionResult> BulkSave([FromBody] List<Phase> Obj)
        {
            //if (Vehicles == null || !Vehicles.Any())
            //     return "BadRequest("No Vehicles provided")";

            //  using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.ModuleData
                        .FirstOrDefaultAsync(v => v.ID == updatedObj.ID);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.Name = existingOBJ.Name;
                        existingOBJ.Code = existingOBJ.Code;
                        existingOBJ.ParentID = existingOBJ.ParentID;
                        existingOBJ.isactive = existingOBJ.isactive;
                    }
                    else
                    {
                        _context.ModuleData.Add(updatedObj);
                    }
                }

                await _context.SaveChangesAsync();
                // await transaction.CommitAsync();

                return null;//Ok(new { message = $"{Vehicles.Count} Vehicles processed successfully" });
            }
            catch (Exception ex)
            {
                // await transaction.RollbackAsync();
                return null;//StatusCode(500, new { error = ex.Message });
            }
        }

    }


    [Route("api/[controller]")]
    [ApiController]
    public class ContractorTypeController : Controller
    {
        private readonly AppDBContext _context;

        public ContractorTypeController(AppDBContext context)
        {
            _context = context;
        }

        // PUT: api/products/5
        [HttpPut("{UpdateContractorType}")]
        public async Task<IActionResult> UpdateContractorType(int id, [FromBody] ContractorType updatedContractorType)
        {
            if (id != updatedContractorType.ID)
            {
                return BadRequest("ContractorType ID mismatch.");
            }

            var existingContractorType = await _context.ModuleData.FindAsync(id);
            if (existingContractorType == null)
            {
                return NotFound();
            }

            // Update properties
            existingContractorType.Name = updatedContractorType.Name;
            existingContractorType.Code = updatedContractorType.Code;
            existingContractorType.ParentID = updatedContractorType.ParentID;
            existingContractorType.isactive = updatedContractorType.isactive;

            _context.Entry(existingContractorType).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingContractorType);
        }


        [HttpPost("AddContractorType")]
        public async Task<IActionResult> AddContractorType([FromBody] ContractorType obj)
        {
            var existingContractorType = await _context.ModuleData.FindAsync(0);
            if (existingContractorType != null)
            {
                return BadRequest("ContractorType Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllContractorTypes()
        {
            //var ContractorTypes = await _context.ContractorTypes.ToListAsync();
            int objval = (int)commonTypes.ModuleTypes.ContractorType;
            var OBJs = await _context.ModuleData.Where(n => n.TypeID == objval && n.ID > objval).ToListAsync();
            return Ok(OBJs);
        }

        [HttpPost("bulkSaveContractorType")]
        public async Task<IActionResult> BulkSave([FromBody] List<ContractorType> Obj)
        {
            //if (Vehicles == null || !Vehicles.Any())
            //     return "BadRequest("No Vehicles provided")";

            //  using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.ModuleData
                        .FirstOrDefaultAsync(v => v.ID == updatedObj.ID);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.Code = updatedObj.Code;
                        existingOBJ.Name = updatedObj.Name;
                        existingOBJ.isactive = updatedObj.isactive;
                        existingOBJ.createdby = 0;
                        existingOBJ.updatedby = 0;
                        existingOBJ.createdon = DateTime.Now;
                    }
                    else
                    {
                        _context.ModuleData.Add(updatedObj);
                    }
                }

                await _context.SaveChangesAsync();
                // await transaction.CommitAsync();

                return null;//Ok(new { message = $"{Vehicles.Count} Vehicles processed successfully" });
            }
            catch (Exception ex)
            {
                // await transaction.RollbackAsync();
                return null;//StatusCode(500, new { error = ex.Message });
            }
        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakeController : Controller
    {
        private readonly AppDBContext _context;

        public VehicleMakeController(AppDBContext context)
        {
            _context = context;
        }


        // PUT: api/products/5
        [HttpPut("{UpdateVehicleMake}")]
        public async Task<IActionResult> UpdateVehicleMake(int id, [FromBody] VehicleMake updatedVehicleMake)
        {
            if (id != updatedVehicleMake.ID)
            {
                return BadRequest("VehicleMake ID mismatch.");
            }

            var existingVehicleMake = await _context.ModuleData.FindAsync(id);
            if (existingVehicleMake == null)
            {
                return NotFound();
            }

            // Update properties
            existingVehicleMake.Name = updatedVehicleMake.Name;
            existingVehicleMake.Code = updatedVehicleMake.Code;
            existingVehicleMake.ParentID = updatedVehicleMake.ParentID;
            existingVehicleMake.isactive = updatedVehicleMake.isactive;
            existingVehicleMake.updatedby = updatedVehicleMake.updatedby;
            existingVehicleMake.updatedon = DateTime.Now;

            _context.Entry(existingVehicleMake).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingVehicleMake);
        }


        [HttpPost("AddVehicleMake")]
        public async Task<IActionResult> AddVehicleMake([FromBody] VehicleMake obj)
        {
            var existingVehicleMake = await _context.ModuleData.FindAsync(0);
            if (existingVehicleMake != null)
            {
                return BadRequest("VehicleMake Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllVehicleMakes()
        {
            int objval = (int)commonTypes.ModuleTypes.VehicleMake;
            var OBJs = await _context.ModuleData.Where(n => n.TypeID == objval && n.ID > objval).ToListAsync();
            return Ok(OBJs);
        }

        [HttpPost("bulkSaveVehicleMake")]
        public async Task<IActionResult> BulkSave([FromBody] List<VehicleMake> Obj)
        {
            //if (Vehicles == null || !Vehicles.Any())
            //     return "BadRequest("No Vehicles provided")";

            //  using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.ModuleData
                        .FirstOrDefaultAsync(v => v.ID == updatedObj.ID);

                    if (existingOBJ != null)
                    {
                        // Update properties 

                        existingOBJ.Code = updatedObj.Code;
                        existingOBJ.Name = updatedObj.Name;
                        existingOBJ.isactive = updatedObj.isactive;
                        existingOBJ.createdby = 0;
                        existingOBJ.updatedby = 0;
                        existingOBJ.createdon = DateTime.Now;
                    }
                    else
                    {
                        _context.ModuleData.Add(updatedObj);
                    }
                }

                await _context.SaveChangesAsync();
                // await transaction.CommitAsync();

                return null;//Ok(new { message = $"{Vehicles.Count} Vehicles processed successfully" });
            }
            catch (Exception ex)
            {
                // await transaction.RollbackAsync();
                return null;//StatusCode(500, new { error = ex.Message });
            }
        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class ReaderLocationController : Controller
    {
        private readonly AppDBContext _context;

        public ReaderLocationController(AppDBContext context)
        {
            _context = context;
        }


        // PUT: api/products/5
        [HttpPut("{UpdateReaderLocation}")]
        public async Task<IActionResult> UpdateReaderLocation(int id, [FromBody] ReaderLocation updatedReaderLocation)
        {
            if (id != updatedReaderLocation.ID)
            {
                return BadRequest("ReaderLocation ID mismatch.");
            }

            var existingReaderLocation = await _context.ModuleData.FindAsync(id);
            if (existingReaderLocation == null)
            {
                return NotFound();
            }

            // Update properties
            existingReaderLocation.Name = updatedReaderLocation.Name;
            existingReaderLocation.Code = updatedReaderLocation.Code;
            existingReaderLocation.ParentID = updatedReaderLocation.ParentID;
            existingReaderLocation.isactive = updatedReaderLocation.isactive;
            existingReaderLocation.updatedon = DateTime.Now;
            existingReaderLocation.updatedby = updatedReaderLocation.updatedby;

            _context.Entry(existingReaderLocation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingReaderLocation);
        }


        [HttpPost("AddReaderLocation")]
        public async Task<IActionResult> AddReaderLocation([FromBody] ReaderLocation obj)
        {
            var existingReaderLocation = await _context.ModuleData.FindAsync(0);
            if (existingReaderLocation != null)
            {
                return BadRequest("ReaderLocation Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllReaderLocations()
        {
            int objval = (int)commonTypes.ModuleTypes.ReaderLocations;
            var OBJs = await _context.ModuleData.Where(n => n.TypeID == objval && n.ID > objval).ToListAsync();
            return Ok(OBJs);
        }

        [HttpPost("bulkSaveReaderLocation")]
        public async Task<IActionResult> BulkSave([FromBody] List<ReaderLocation> Obj)
        {
            //if (Vehicles == null || !Vehicles.Any())
            //     return "BadRequest("No Vehicles provided")";

            //  using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.ModuleData
                        .FirstOrDefaultAsync(v => v.ID == updatedObj.ID);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.Code = updatedObj.Code;
                        existingOBJ.Name = updatedObj.Name;
                        existingOBJ.isactive = updatedObj.isactive;
                        existingOBJ.updatedby = updatedObj.updatedby;
                        existingOBJ.updatedon = DateTime.Now;
                    }
                    else
                    {
                        _context.ModuleData.Add(updatedObj);
                    }
                }

                await _context.SaveChangesAsync();
                // await transaction.CommitAsync();

                return null;//Ok(new { message = $"{Vehicles.Count} Vehicles processed successfully" });
            }
            catch (Exception ex)
            {
                // await transaction.RollbackAsync();
                return null;//StatusCode(500, new { error = ex.Message });
            }
        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class ReaderRelayController : Controller
    {
        private readonly AppDBContext _context;

        public ReaderRelayController(AppDBContext context)
        {
            _context = context;
        }


        // PUT: api/products/5
        [HttpPut("{UpdateReaderRelay}")]
        public async Task<IActionResult> UpdateReaderRelay(int id, [FromBody] ReaderRelay updatedReaderRelay)
        {
            if (id != updatedReaderRelay.ID)
            {
                return BadRequest("ReaderRelay ID mismatch.");
            }

            var existingReaderRelay = await _context.ModuleData.FindAsync(id);
            if (existingReaderRelay == null)
            {
                return NotFound();
            }

            // Update properties
            existingReaderRelay.Name = updatedReaderRelay.Name;
            existingReaderRelay.Code = updatedReaderRelay.Code;
            existingReaderRelay.ParentID = updatedReaderRelay.ParentID;
            existingReaderRelay.isactive = updatedReaderRelay.isactive;
            existingReaderRelay.updatedby = updatedReaderRelay.updatedby;
            existingReaderRelay.updatedon = DateTime.Now;

            _context.Entry(existingReaderRelay).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingReaderRelay);
        }


        [HttpPost("AddReaderRelay")]
        public async Task<IActionResult> AddReaderRelay([FromBody] ReaderRelay obj)
        {
            var existingReaderRelay = await _context.ModuleData.FindAsync(0);
            if (existingReaderRelay != null)
            {
                return BadRequest("ReaderRelay Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllReaderRelays()
        {
            int objval = (int)commonTypes.ModuleTypes.ReaderRelays;
            var OBJs = await _context.ModuleData.Where(n => n.TypeID == objval && n.ID > objval).ToListAsync();
            return Ok(OBJs);
        }

        [HttpPost("bulkSaveReaderRelay")]
        public async Task<IActionResult> BulkSave([FromBody] List<ReaderRelay> Obj)
        {
            //if (Vehicles == null || !Vehicles.Any())
            //     return "BadRequest("No Vehicles provided")";

            //  using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.ModuleData
                        .FirstOrDefaultAsync(v => v.ID == updatedObj.ID);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.Code = updatedObj.Code;
                        existingOBJ.Name = updatedObj.Name;
                        existingOBJ.isactive = updatedObj.isactive;
                        existingOBJ.updatedby = updatedObj.updatedby;
                        existingOBJ.updatedon = DateTime.Now;
                    }
                    else
                    {
                        _context.ModuleData.Add(updatedObj);
                    }
                }

                await _context.SaveChangesAsync();
                // await transaction.CommitAsync();

                return null;//Ok(new { message = $"{Vehicles.Count} Vehicles processed successfully" });
            }
            catch (Exception ex)
            {
                // await transaction.RollbackAsync();
                return null;//StatusCode(500, new { error = ex.Message });
            }
        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : Controller
    {
        private readonly AppDBContext _context;

        public ServiceTypeController(AppDBContext context)
        {
            _context = context;
        }


        // PUT: api/products/5
        [HttpPut("{UpdateServiceType}")]
        public async Task<IActionResult> UpdateServiceType(int id, [FromBody] ServiceType updatedServiceType)
        {
            if (id != updatedServiceType.ID)
            {
                return BadRequest("ServiceType ID mismatch.");
            }

            var existingServiceType = await _context.ModuleData.FindAsync(id);
            if (existingServiceType == null)
            {
                return NotFound();
            }

            // Update properties
            existingServiceType.Name = updatedServiceType.Name;
            existingServiceType.Code = updatedServiceType.Code;
            existingServiceType.ParentID = updatedServiceType.ParentID;
            existingServiceType.isactive = updatedServiceType.isactive;

            _context.Entry(existingServiceType).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingServiceType);
        }


        [HttpPost("AddServiceType")]
        public async Task<IActionResult> AddServiceType([FromBody] ServiceType obj)
        {
            var existingServiceType = await _context.ModuleData.FindAsync(0);
            if (existingServiceType != null)
            {
                return BadRequest("ServiceType Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllServiceTypes()
        {
            int objval = (int)commonTypes.ModuleTypes.ServiceType;
            var OBJs = await _context.ModuleData.Where(n => n.TypeID == objval && n.ID > objval).ToListAsync();
            return Ok(OBJs);
        }

        [HttpPost("bulkSaveServiceType")]
        public async Task<IActionResult> BulkSave([FromBody] List<ServiceType> Obj)
        {
            //if (Vehicles == null || !Vehicles.Any())
            //     return "BadRequest("No Vehicles provided")";

            //  using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.ModuleData
                        .FirstOrDefaultAsync(v => v.ID == updatedObj.ID);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.Code = updatedObj.Code;
                        existingOBJ.Name = updatedObj.Name;
                        existingOBJ.isactive = updatedObj.isactive;
                        existingOBJ.updatedby = updatedObj.updatedby;
                        existingOBJ.updatedon = DateTime.Now;
                    }
                    else
                    {
                        _context.ModuleData.Add(updatedObj);
                    }
                }

                await _context.SaveChangesAsync();
                // await transaction.CommitAsync();

                return null;//Ok(new { message = $"{Vehicles.Count} Vehicles processed successfully" });
            }
            catch (Exception ex)
            {
                // await transaction.RollbackAsync();
                return null;//StatusCode(500, new { error = ex.Message });
            }
        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : Controller
    {
        private readonly AppDBContext _context;

        public BuildingController(AppDBContext context)
        {
            _context = context;
        }

        // PUT: api/products/5
        [HttpPut("{UpdateBuilding}")]
        public async Task<IActionResult> UpdateBuilding(int id, [FromBody] Building updatedBuilding)
        {
            if (id != updatedBuilding.ID)
            {
                return BadRequest("Building ID mismatch.");
            }

            var existingBuilding = await _context.ModuleData.FindAsync(id);
            if (existingBuilding == null)
            {
                return NotFound();
            }

            // Update properties
            existingBuilding.Name = updatedBuilding.Name;
            existingBuilding.Code = updatedBuilding.Code;
            existingBuilding.ParentID = updatedBuilding.ParentID;
            existingBuilding.isactive = updatedBuilding.isactive;
            existingBuilding.updatedby = updatedBuilding.updatedby;
            existingBuilding.updatedon = DateTime.Now;

            _context.Entry(existingBuilding).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingBuilding);
        }


        [HttpPost("AddBuilding")]
        public async Task<IActionResult> AddBuilding([FromBody] Building obj)
        {
            var existingBuilding = await _context.ModuleData.FindAsync(0);
            if (existingBuilding != null)
            {
                return BadRequest("Building Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllBuildings()
        {
            int objval = (int)commonTypes.ModuleTypes.Building;
            var OBJs = await _context.ModuleData.Where(n => n.TypeID == objval && n.ID > objval).ToListAsync();

            return Ok(OBJs);
        }


        [HttpPost("bulkSaveBuilding")]
        public async Task<IActionResult> BulkSave([FromBody] List<Building> Obj)
        {
            //if (Vehicles == null || !Vehicles.Any())
            //     return "BadRequest("No Vehicles provided")";

            //using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.ModuleData
                        .FirstOrDefaultAsync(v => v.ID == updatedObj.ID);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.Name = updatedObj.Name;
                        existingOBJ.Code = updatedObj.Code;
                        existingOBJ.ParentID = updatedObj.ParentID;
                        existingOBJ.isactive = updatedObj.isactive;
                        existingOBJ.updatedby = updatedObj.updatedby;
                        existingOBJ.updatedon = DateTime.Now;
                    }
                    else
                    {
                        _context.ModuleData.Add(updatedObj);
                    }
                }

                await _context.SaveChangesAsync();
                // await transaction.CommitAsync();

                return null;//Ok(new { message = $"{Vehicles.Count} Vehicles processed successfully" });
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();
                return null;//StatusCode(500, new { error = ex.Message });
            }
        }


    }

    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : Controller
    {
        private readonly AppDBContext _context;

        public AmenitiesController(AppDBContext context)
        {
            _context = context;
        }


        // PUT: api/products/5
        [HttpPut("{UpdateAmenities}")]
        public async Task<IActionResult> UpdateAmenities(int id, [FromBody] Amenities updatedAmenities)
        {
            if (id != updatedAmenities.ID)
            {
                return BadRequest("Amenities ID mismatch.");
            }

            var existingAmenities = await _context.ModuleData.FindAsync(id);
            if (existingAmenities == null)
            {
                return NotFound();
            }

            // Update properties
            existingAmenities.Name = updatedAmenities.Name;
            existingAmenities.Code = updatedAmenities.Code;
            existingAmenities.ParentID = updatedAmenities.ParentID;
            existingAmenities.isactive = updatedAmenities.isactive;
            existingAmenities.updatedby = updatedAmenities.updatedby;
            existingAmenities.updatedon = DateTime.Now;

            _context.Entry(existingAmenities).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingAmenities);
        }


        [HttpPost("AddAmenities")]
        public async Task<IActionResult> AddAmenities([FromBody] Amenities obj)
        {
            var existingAmenities = await _context.ModuleData.FindAsync(0);
            if (existingAmenities != null)
            {
                return BadRequest("Amenities Exists.");
            }
            _context.Add(obj); //_context.Add(obj);await _context.SaveChangesAsync();


            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllAmenities()
        {
            int objval = (int)commonTypes.ModuleTypes.Amenities;
            var OBJs = await _context.ModuleData.Where(n => n.TypeID == objval && n.ID > objval).ToListAsync();
            return Ok(OBJs);
        }

     
        [HttpPost("bulkSaveAmenities")]
        public async Task<IActionResult> BulkSave([FromBody] List<Amenities> Obj)
        {
            //if (Vehicles == null || !Vehicles.Any())
            //     return "BadRequest("No Vehicles provided")";

            //    using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.ModuleData
                        .FirstOrDefaultAsync(v => v.ID == updatedObj.ID);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.Name = updatedObj.Name;
                        existingOBJ.Code = updatedObj.Code;
                        existingOBJ.ParentID = updatedObj.ParentID;
                        existingOBJ.isactive = updatedObj.isactive;
                        existingOBJ.updatedby = updatedObj.updatedby;
                        existingOBJ.updatedon = DateTime.Now;

                    }
                    else
                    {
                        _context.ModuleData.Add(updatedObj);
                    }
                }

                await _context.SaveChangesAsync();
                //        await transaction.CommitAsync();

                return null;//Ok(new { message = $"{Vehicles.Count} Vehicles processed successfully" });
            }
            catch (Exception ex)
            {
                // await transaction.RollbackAsync();
                return null;//StatusCode(500, new { error = ex.Message });
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly AppDBContext _context;

        public VehicleController(AppDBContext context)
        {
            _context = context;
        }


        // PUT: api/products/5
        [HttpPut("{UpdateVehicle}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] Vehicle updatedVehicle)
        {
            if (id != updatedVehicle.ID)
            {
                return BadRequest("Vehicle ID mismatch.");
            }
            var existingVehicle = await _context.Vehicles.FindAsync(id);
            if (existingVehicle == null)
            {
                return NotFound();
            }

            _context.Entry(existingVehicle).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingVehicle);
        }


        [HttpPost("AddVehicle")]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle obj)
        {
            var existingVehicle = await _context.Vehicles.FindAsync(0);
            if (existingVehicle != null)
            {
                return BadRequest("Vehicle Exists.");
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var Vehicles = await _context.Vehicles.ToListAsync();
            return Ok(Vehicles);
        }

        [HttpPost("bulkSaveVehicles")]
        public async Task<IActionResult> BulkSave([FromBody] List<Vehicle> Obj)
        {
            //if (Vehicles == null || !Vehicles.Any())
            //     return "BadRequest("No Vehicles provided")";

            // using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var updatedObj in Obj)
                {
                    var existingOBJ = await _context.Vehicles
                        .FirstOrDefaultAsync(v => v.ID == updatedObj.ID);

                    if (existingOBJ != null)
                    {
                        // Update properties 
                        existingOBJ.RegNo = updatedObj.RegNo;
                        existingOBJ.vType = updatedObj.vType;
                        existingOBJ.vMake = updatedObj.vMake;
                        existingOBJ.vColor = updatedObj.vColor;
                        existingOBJ.TagUID = updatedObj.TagUID;
                        existingOBJ.TagEncodingDate = updatedObj.TagEncodingDate;
                        existingOBJ.Logical_Delete = updatedObj.Logical_Delete;
                        existingOBJ.PrintedTagID = updatedObj.PrintedTagID;
                        existingOBJ.StickerNo = updatedObj.StickerNo;


                    }
                    else
                    {
                        _context.Vehicles.Add(updatedObj);
                    }
                }

                await _context.SaveChangesAsync();
                //await transaction.CommitAsync();

                return null;//Ok(new { message = $"{Vehicles.Count} Vehicles processed successfully" });
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();
                return null;//StatusCode(500, new { error = ex.Message });
            }
        }
    }

}
