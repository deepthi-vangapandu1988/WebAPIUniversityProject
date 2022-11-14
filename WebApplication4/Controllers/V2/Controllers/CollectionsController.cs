using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data.Repository;
using WebApplication4.Data;
using WebApplication4.Models;
using System.Linq;

namespace WebApplication4.Controllers.V2.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CollectionsController : ControllerBase
    {
        private readonly UniversityDbContext _db;
        public CollectionsController(UniversityDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<string> GetAsync()
        {
            return "2nd version";
        }
    }
}
