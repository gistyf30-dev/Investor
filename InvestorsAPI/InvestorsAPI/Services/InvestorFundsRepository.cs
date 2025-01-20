using InvestorsAPI.Database;
using InvestorsAPI.Entities;
using InvestorsAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace InvestorsAPI.Services
{
    public class InvestorFundsRepository: IInvestorFundsRepository
    {
        private InvestorDbContext _dbContext;

        public InvestorFundsRepository(InvestorDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public ICollection<Investor> GetAllInvestors()
        {
            return _dbContext.Investors
                 .Include(x=>x.InvestorFunds)
                 .ThenInclude(f => f.Fund)
                .ToList();
        }

        public ICollection<Fund> getAllFunds()
        {
            return _dbContext.Funds
                .ToList();
        }

        public Investor GetInvestorById(int investorId)
        {
            return _dbContext.Investors
                 .Include(x => x.InvestorFunds)
                 .ThenInclude(f => f.Fund)
                 .Where(x => x.InvestorId == investorId)
                .FirstOrDefault();
        }

        public bool CreateInvestorFund(InvestorFund investorFund)
        {
            try
            {
                _dbContext.Add(investorFund);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return true;
        }
        public bool CreateInvestor(Investor investor)
        {
            try
            {
                _dbContext.Add(investor);
               _dbContext.SaveChanges();
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());   
            }

            return true;
        }
        public List<Fund> GetFundsByInvestorId(int investorId)
        {
            return _dbContext.InvestorFunds.Where(a => a.Investor.InvestorId == investorId).Select(b => b.Fund).ToList();
        }
        public bool InvestorExists(int investorId)
        {
            return _dbContext.Investors.Any(a => a.InvestorId == investorId);
        }
        public bool DeleteInvestor(Investor investor)
        {
            _dbContext.Remove(investor);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }
    }
}
