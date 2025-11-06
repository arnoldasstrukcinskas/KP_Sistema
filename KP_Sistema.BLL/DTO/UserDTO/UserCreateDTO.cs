using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.DTO.UserDTO
{
    public class UserCreateDTO
    {
        public string Username { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int CommunityId { get; set; }
        public Community Community { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
