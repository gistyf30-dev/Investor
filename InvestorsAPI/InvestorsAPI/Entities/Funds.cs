using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorsAPI.Entities
{
    [PrimaryKey("FundId")]
    public class Fund
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FundId { get; set; } 
        public string? FundName { get; set; }    
        public List<InvestorFund>? InvestorFunds { get; set; }
    }
}
