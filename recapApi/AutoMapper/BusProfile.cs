using AutoMapper;
using Dtos.Bus;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recapApi.AutoMapper
{
    public class BusProfile : Profile
    {
        public BusProfile()
        {
            CreateMap<Bus, BusDto>().ReverseMap();
        }
    }
}
