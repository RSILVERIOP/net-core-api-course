using AutoMapper;
using Api.Domain.Entites;
using Api.Domain.Dtos.User;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {   
            CreateMap<UserDto, UserEntity>()
                .ReverseMap();

            CreateMap<UseDtoCreateResult, UserEntity>()
                .ReverseMap();
            
            CreateMap<UseDtoUpdateResult, UserEntity>()
                .ReverseMap();
        }
    }
}