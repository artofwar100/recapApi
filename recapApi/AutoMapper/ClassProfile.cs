using AutoMapper;
using Dtos.Class;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recapApi.AutoMapper
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Class, ClassDtoCreateUpdate>().ReverseMap();
        }
    }
}
