using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.DTO.UtilityTaskDTO
{
    public class UtilityTaskCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CommunityId { get; set; }
        public Community Community { get; set; }
    }
}
