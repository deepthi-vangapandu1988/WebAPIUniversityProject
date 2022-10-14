using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
            : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    Id = 1,
                    Name = "Student 1",
                    Email = "Student1@gmail.com",
                    Address = "HYD"
                },
                new Student()
                {
                    Id = 2,
                    Name = "Student 2",
                    Email = "Student2@gmail.com",
                    Address = "HYD"
                },
                new Student()
                {
                    Id = 3,
                    Name = "Student 3",
                    Email = "Student2@gmail.com",
                    Address = "HYD"
                },
                new Student()
                {
                    Id = 4,
                    Name = "Student 4",
                    Email = "Student3@gmail.com",
                    Address = "HYD"
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Fullname = "Student 1",
                    Username = "Student1",
                    Password = "Password1",
                    Role = "Student"
                },
                new User
                {
                    Id = 2,
                    Fullname = "Student 2",
                    Username = "Student2",
                    Password = "Password2",
                    Role = "Student"
                },
                new User
                {
                    Id = 3,
                    Fullname = "Student 3",
                    Username = "Student3",
                    Password = "Password3",
                    Role = "Student"
                });
        }
    }
}
