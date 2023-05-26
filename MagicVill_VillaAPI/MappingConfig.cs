using AutoMapper;
using MagicVill_VillaAPI.Models;
using MagicVill_VillaAPI.Models.DTO;

namespace MagicVill_VillaAPI
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            //Villa maping
            CreateMap<Villa,VillaDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();

            //Villa No Mapping
            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();

            //User Mapping
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();

        }
    }
}
