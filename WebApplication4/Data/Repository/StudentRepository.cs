using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Data.Repository
{
    public class StudentRepository : UniversityRepository<Student>, IStudentRepository
    {
        private readonly UniversityDbContext _db;
        public StudentRepository(UniversityDbContext db) : base(db)
        {
            _db = db;
        }
        
    }
}
