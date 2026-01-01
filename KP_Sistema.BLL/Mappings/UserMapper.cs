using AutoMapper;
using KP_Sistema.CONTRACTS.DTO.UserDTO;
using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Mappings
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserTransferDTO>()
                .ForMember(destination => destination.CommunityName, option => option.MapFrom(source => source.Community.Name))
                .ForMember(destination => destination.Role, option => option.MapFrom(source => source.Role.Name));
            CreateMap<UserTransferDTO, UserReturnDTO>();

            //CreateMap<User, UserCreateDTO>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<User, UserReturnDTO>()
                .ForMember(destination => destination.Role, option => option.MapFrom(source => source.Role.Name))
                .ForMember(destination => destination.Community, option => option.MapFrom(source => source.Community.Name));
            CreateMap<CurrentUserDTO, UserReturnDTO>();
        }
    }
}
