using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorsAPI.Entities
{
    public class InvestorFund
    {
        [ForeignKey("InvestorId")]
        public int InvestorId { get; set; }
        public Investor? Investor { get; set; }
        [ForeignKey("FundId")]
        public int FundId { get; set; }
        public Fund? Fund { get; set; }        
    }
}
