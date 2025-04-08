using Microsoft.AspNetCore.Mvc;
using Township_API.Data;
using Township_API.Services; 
using Township_API.Models; 
using Microsoft.EntityFrameworkCore;

namespace Township_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderController : Controller
    {
        private readonly AppDBContext _context;
         

        public ServiceProviderController(AppDBContext context)
        {
            _context = context; 
        }


    

}  
     

}
