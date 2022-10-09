using WebApplication4.Dto;

namespace WebApplication4.Data
{
    public static class UniversityStore
    {
        public static List<StudentDTO> Students = new List<StudentDTO>()
            {
                new StudentDTO()
                {
                    Id = 1,
                    Name = "Student 1"
                },
                new StudentDTO()
                {
                    Id = 2,
                    Name = "Student 2"
                }
            };
    }
}
