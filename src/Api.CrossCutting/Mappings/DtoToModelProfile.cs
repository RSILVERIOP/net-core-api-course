using AutoMapper;
using Api.Domain.Models;
using  Api.Domain.Dtos.User;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDto>()
                .ReverseMap();
        }
    }
}