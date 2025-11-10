using AutoMapper;
using KP_Sistema.BLL.DTO.UserDTO;
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
            CreateMap<User, UserCreateDTO>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<User, UserReturnDTO>();
        }
    }
}
