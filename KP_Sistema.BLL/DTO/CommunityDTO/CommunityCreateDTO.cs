using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.DTO.CommunityDTO
{
    public class CommunityCreateDTO
    {
        public string Name { get; set; }
        public List<UtilityTask>? UtilityTasks { get; set; }
        public List<User>? Users { get; set; }
    }
}
