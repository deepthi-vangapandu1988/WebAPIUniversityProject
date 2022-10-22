using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data.Repository;
using WebApplication4.Data;
using WebApplication4.Models;
using System.Linq;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly UniversityDbContext _db;
        public CollectionsController(UniversityDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetStudentsAsync()
        {
            try
            {
                IQueryable<Student> query = _db.Students;
                List<Student> list = new List<Student>();
                if (true)//string.isnullorempty(model.fname)
                {
                    query = query.Where(n => n.Name == "abc");
                }
                if (true)//string.isnullorempty(model.phone)
                {
                    query = query.Where(n => n.Address == "ddd");
                }

                list = query.ToList();

                IEnumerable<Student> lis = _db.Students;
                    var d = lis.Where(n => n.Id % 2 == 0).ToList();





                Console.WriteLine("IEnumerable-Calling");
                IEnumerable<Student> students_En = _db.Students;
                Console.WriteLine("ICollection-Calling");
                ICollection<Student> students_Co = _db.Students.ToList();
                Console.WriteLine("IList-Calling");
                IList<Student> students_Li = _db.Students.ToList();

                //IQueryable<Student> ss = students_Li.Where(n => n.Id == 0); not possible

                var list1 = students_En.Where(n => n.Id %2 ==0).ToList();
                var list2 = students_En.Where(n => n.Id %2 !=0).ToList();
                var list3 = students_Co.Where(n => n.Id %2 !=0).ToList();
                foreach (var item in students_En)
                {
                    Console.WriteLine(item.Name);
                }





















                return new APIResponse(System.Net.HttpStatusCode.OK, true, null);
            }
            catch (Exception ex)
            {
                return new APIResponse(System.Net.HttpStatusCode.InternalServerError, false, ex.Message);
            }
            return null;
        }
    }
}
