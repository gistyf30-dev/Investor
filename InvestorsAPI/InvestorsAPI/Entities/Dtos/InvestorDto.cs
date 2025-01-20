namespace InvestorsAPI.Entities.Dtos
{
    public class InvestorDto
    {
        public int InvestorId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }        
        public List<FundDto> Funds { get; set; }
    }
}
