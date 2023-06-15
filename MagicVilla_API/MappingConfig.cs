using AutoMapper;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;

namespace MagicVilla_API
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {
            //Sin ReverseMap
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();

            CreateMap<NumberVilla, NumberVillaDto>().ReverseMap();
            CreateMap<NumberVilla, NumberVillaCreateDto>().ReverseMap();
            CreateMap<NumberVilla, NumberVillaUpdateDto>().ReverseMap();
        }
    }
}
