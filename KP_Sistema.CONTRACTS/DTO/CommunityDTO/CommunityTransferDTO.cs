using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KP_Sistema.CONTRACTS.DTO.UserDTO;
using KP_Sistema.CONTRACTS.DTO.UtilityTaskDTO;

namespace KP_Sistema.CONTRACTS.DTO.CommunityDTO
{
    public class CommunityTransferDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<UtilityTaskTransferDTO>? UtilityTasks { get; set; }
        public ICollection<UserTransferDTO>? Users { get; set; }
    }
}
