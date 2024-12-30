using AutoMapper;
using Testify.Core.DTOs.User;
using Testify.Core.Models;

namespace TestifyWebAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUserDto>();
        }
    }
}
