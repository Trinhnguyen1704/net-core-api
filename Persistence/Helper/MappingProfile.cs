using System;
using AutoMapper;
using net_core_api.Models;
using net_core_api.Models.DTOs;

namespace net_core_api.Persistence.Helper
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<ClassDTO, Class>();
        }
    }
}