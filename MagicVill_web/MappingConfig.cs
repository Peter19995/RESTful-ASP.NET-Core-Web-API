using AutoMapper;
using MagicVill_web.Models;

namespace MagicVill_web
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            //Villa maping
            
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
          

            //Villa No Mapping
            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();

        }
    }
}
