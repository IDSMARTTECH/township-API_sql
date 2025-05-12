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
using System.Reflection.Metadata.Ecma335;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly AppDBContext _context;
        public EmployeeController(AppDBContext context)
        {
            _context = context;
        }

        // PUT: api/products/5
        [HttpPost("{UpdateEmployee}/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            try
            {
                if (id != updatedEmployee.ID)
                {
                    return BadRequest("Employee ID mismatch.");
                }

                //var existingEmployee = await _service.UpdateEmployeeAsync(updatedEmployee.ID, updatedEmployee);
                var existingEmployee = await _context.Employees.FindAsync(updatedEmployee.ID);

                if (existingEmployee == null)
                {
                    return NotFound();
                }
                existingEmployee.ID = updatedEmployee.ID;
                existingEmployee.code = updatedEmployee.code;
                existingEmployee.name = updatedEmployee.name;
                existingEmployee.email = updatedEmployee.email;
                existingEmployee.phone = updatedEmployee.phone; 
                existingEmployee.role = updatedEmployee.role;
                existingEmployee.isactive = updatedEmployee.isactive;
                existingEmployee.createdby = updatedEmployee.createdby;
                existingEmployee.createdon = updatedEmployee.createdon;
                existingEmployee.updatedby = updatedEmployee.updatedby;
                existingEmployee.updatedon = updatedEmployee.updatedon;
                _context.Entry(existingEmployee).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(existingEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee obj)
        {
            try
            {
                var existingEmployee = await _context.Employees.FindAsync(0);
                if (existingEmployee != null)
                {
                    return BadRequest("Employee Exists.");
                }

                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblEmployee ON");
                _context.Add(obj);
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblEmployee OFF");
                await _context.SaveChangesAsync();
                int number = (int)AccessCardHilders.Employee;
                obj.code = number.ToString() + obj.ID.ToString("D9");

                await _context.SaveChangesAsync();
                return Ok(new { message = $"{obj.code} Employee Added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        // GET: api/products 
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var Employees = await _context.Employees.OrderByDescending(p => p.ID).ToListAsync();
            return Ok(Employees);
        }


        // GET: api/products 
        [HttpGet("GetEmployeeByID/{id}")]
        public async Task<IActionResult> GetEmployeeByID(int id)
        {
            var Employees = await _context.Employees.Where(p=>p.ID ==id).OrderByDescending(p => p.ID).ToListAsync();
            if (Employees == null) 
                   return BadRequest("No Data found");

            
            return Ok(Employees);
        }


        [HttpPost("AddEmployees")]
        public async Task<IActionResult> AddEmployees([FromBody] List<Employee> Obj)
        {
            if (Obj == null || !Obj.Any())
                return BadRequest("No Data provided");

            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                foreach (var objID in Obj)
                {
                    var existingobj = await _context.Employees
                        .FirstOrDefaultAsync(p => p.ID == objID.ID);

                    if (existingobj != null)
                    {
                        //  var existingEmployee = await _service.UpdateEmployeeAsync(objID.ID, existingobj);
                        var existingEmployee = await _context.Employees.FindAsync(0);
                        if (existingEmployee == null)
                        {
                            return NotFound();
                        }
                    }
                    else
                    { 
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblEmployee ON");
                        _context.Employees.Add(objID);
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tblEmployee  OFF");
                        await _context.SaveChangesAsync();
                        int number = (int)AccessCardHilders.Employee;
                        objID.code = number.ToString() + objID.ID.ToString("D9");
                        await _context.SaveChangesAsync();
                    }
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync(); 

                return Ok(new { message = $"{Obj.Count} Employee processed successfully" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }

}
