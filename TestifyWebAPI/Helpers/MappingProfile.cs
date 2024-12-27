using AutoMapper;
using Testify.Core.Models;
using TestifyWebAPI.DTOs;

namespace TestifyWebAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
