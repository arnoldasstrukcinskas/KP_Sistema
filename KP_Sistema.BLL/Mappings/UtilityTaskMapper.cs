using AutoMapper;
using KP_Sistema.BLL.DTO.UtilityTaskDTO;
using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.Mappings
{
    public class UtilityTaskMapper : Profile
    {
        public UtilityTaskMapper()
        {
            CreateMap<UtilityTaskCreateDTO, UtilityTask>();
            CreateMap<UtilityTask, UtilityTaskTransferDTO>()
                .ForMember(destination => destination.CommunityName, option => option.MapFrom(source => source.Community.Name));
            CreateMap<UtilityTask, UtilityTaskReturnDTO>()
                .ForMember(destination => destination.CommunityName, option => option.MapFrom(source => source.Community.Name));
            CreateMap<UtilityTaskTransferDTO, UtilityTaskReturnDTO>();
            CreateMap<UtilityTaskTransferDTO, UtilityTask>();
            
        }
    }
}
