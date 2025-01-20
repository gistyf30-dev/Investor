using InvestorsAPI.Entities;

namespace InvestorsAPI.Services.Interface
{
    public interface IInvestorFundsRepository
    {
        public bool CreateInvestorFund(InvestorFund investorFund);
        public ICollection<Investor> GetAllInvestors();

        public ICollection<Fund> getAllFunds();
        public Investor GetInvestorById(int investorId);
        public bool DeleteInvestor(Investor investor);
        public List<Fund> GetFundsByInvestorId(int investorId);
        public bool InvestorExists(int investorId);

        public bool CreateInvestor(Investor investor);
    }
}
