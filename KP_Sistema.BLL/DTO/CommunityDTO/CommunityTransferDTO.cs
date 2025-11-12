using KP_Sistema.BLL.DTO.UserDTO;
using KP_Sistema.BLL.DTO.UtilityTaskDTO;
using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.DTO.CommunityDTO
{
    public class CommunityTransferDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UtilityTaskTransferDTO>? UtilityTasks { get; set; }
        public ICollection<UserTransferDTO>? Users { get; set; }
    }
}
