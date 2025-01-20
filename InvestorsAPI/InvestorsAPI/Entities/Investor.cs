using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InvestorsAPI.Entities
{
    [PrimaryKey("InvestorId")]
    public class Investor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int InvestorId { get; set; } 
        public string? Name { get; set; }
        public string? Phone { get; set; }  
        public string? Email { get; set; }
        public string? Country { get; set; }

        public ICollection<InvestorFund>? InvestorFunds { get; set; }
    }
}
