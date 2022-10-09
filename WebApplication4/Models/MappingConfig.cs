using AutoMapper;
using System.Collections.Immutable;
using WebApplication4.Data;
using WebApplication4.Dto;

namespace WebApplication4.Models
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();

            CreateMap<StudentCreateDTO, Student>().ReverseMap();
            CreateMap<StudentUpdateDTO, Student>().ReverseMap();
        }
    }
}
