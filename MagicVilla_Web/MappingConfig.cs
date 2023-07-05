﻿using AutoMapper;
using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();

            CreateMap<NumberVillaDto, NumberVillaCreateDto>().ReverseMap();
            CreateMap<NumberVillaDto, NumberVillaUpdateDto>().ReverseMap();
        }
    }
}
