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
            CreateMap<CommunityTransferDTO, Community>();
            CreateMap<CommunityTransferDTO, CommunityTransferDTO>();
            CreateMap<CommunityTransferDTO, CommunityReturnDTO>();
            CreateMap<Community, CommunityTransferDTO>();
            CreateMap<CommunityTransferDTO, Community>();
        }
    }
}
