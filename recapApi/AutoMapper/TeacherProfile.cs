using AutoMapper;
using Dtos.Teacher;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recapApi.AutoMapper
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherDto>().ReverseMap();
        }
    }
}
