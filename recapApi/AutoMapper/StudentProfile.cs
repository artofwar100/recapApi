using AutoMapper;
using Dtos.Student;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recapApi.AutoMapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Student, StudentDtoCreateUpdate>().ReverseMap();
        }
    }
}
