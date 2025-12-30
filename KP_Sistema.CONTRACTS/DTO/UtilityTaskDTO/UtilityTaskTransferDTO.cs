namespace KP_Sistema.CONTRACTS.DTO.UtilityTaskDTO
{
    public class UtilityTaskTransferDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CommunityId { get; set; }
        public string CommunityName { get; set; }
    }
}
