using KP_Sistema.CONTRACTS.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.CONTRACTS.DTO.AuthenticationDTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public UserReturnDTO user { get; set; }
    }
}
