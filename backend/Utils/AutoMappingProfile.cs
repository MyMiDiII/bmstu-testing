using AutoMapper;
using ServerING.DTO;
using ServerING.Models;
using ServerING.ModelsBL;

namespace ServerING.Utils {
    public class AutoMappingProfile : Profile {
        public AutoMappingProfile() {			
            // DB <-> BL
            CreateMap<Country, CountryBL>().ReverseMap();
            CreateMap<FavoriteServer, FavoriteServerBL>().ReverseMap();
            CreateMap<Platform, PlatformBL>().ReverseMap();
            CreateMap<Player, PlayerBL>().ReverseMap();
            CreateMap<Server, ServerBL>().ReverseMap();
            CreateMap<ServerPlayer, ServerPlayerBL>().ReverseMap();
            CreateMap<User, UserBL>().ReverseMap();
            CreateMap<WebHosting, WebHostingBL>().ReverseMap();

            // DTO <-> BL
            CreateMap<ServerDtoBase, ServerBL>().ReverseMap();
            CreateMap<ServerDto, ServerBL>().ReverseMap();
            CreateMap<FavoriteServerDtoBase, FavoriteServerBL>().ReverseMap();
            CreateMap<FavoriteServerDto, FavoriteServerBL>().ReverseMap();
            CreateMap<PlatformBaseDto, PlatformBL>().ReverseMap();
            CreateMap<PlatformDto, PlatformBL>().ReverseMap();
            CreateMap<CountryBaseDto, CountryBL>().ReverseMap();
            CreateMap<CountryDto, CountryBL>().ReverseMap();
            CreateMap<HostingBaseDto, WebHostingBL>().ReverseMap();
            CreateMap<HostingDto, WebHostingBL>().ReverseMap();
            CreateMap<PlayerBaseDto, PlayerBL>().ReverseMap();
            CreateMap<PlayerDto, PlayerBL>().ReverseMap();
            CreateMap<UserBaseDto, UserBL>().ReverseMap();
            CreateMap<UserDto, UserBL>().ReverseMap();
            CreateMap<UserPasswordDto, UserBL>().ReverseMap();
            CreateMap<UserIdPasswordDto, UserBL>().ReverseMap();
        }
    }
}
