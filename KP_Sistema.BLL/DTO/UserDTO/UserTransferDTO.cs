using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.DTO.UserDTO
{
    public class UserTransferDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }
        public string CommunityName { get; set; }
        public string Role { get; set; }
    }
}
