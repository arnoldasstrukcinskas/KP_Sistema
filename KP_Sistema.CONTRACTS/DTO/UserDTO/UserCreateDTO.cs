namespace KP_Sistema.CONTRACTS.DTO.UserDTO
{
    public class UserCreateDTO
    {
        public string Username { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
