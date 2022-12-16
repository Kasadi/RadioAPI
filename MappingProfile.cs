using AutoMapper;
using RadioAPI.Data.Dto.StationFolder;
using RadioAPI.Model;

namespace RadioAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Station, FavoriteListDto>();
            CreateMap<FavoriteListDto, Station>();
        }
    }
}
