using AutoMapper;
using KP_Sistema.BLL.DTO.CommunityDTO;
using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Mappings
{
    public class CommunityMapper : Profile
    {
        public CommunityMapper()
        {
            CreateMap<CommunityCreateDTO, Community>();
            CreateMap<Community, CommunityTransferDTO>();
            CreateMap<Community, CommunityReturnDTO>();
            CreateMap<CommunityTransferDTO, Community>()
                .ForMember(destination => destination.Users, option => option.Ignore())
                .ForMember(destination => destination.UtilityTasks, option => option.Ignore());
            CreateMap<CommunityTransferDTO, CommunityReturnDTO>();
        }
    }
}
