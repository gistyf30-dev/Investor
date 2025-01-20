using Investor.Models;

namespace Investor.Services.Interfaces
{
    public interface IInvestorRepository
    {
        Task<string> GetInvestors();
        Task<string> GetFundList();

        Task<string> GetInvestorById(int investorId);
        Task<string> AddInvestor(InvestorRequest ObjClsInvestor);

        Task<string> AddFundToInvestor(InvestorFund ObjClsInvestor);

        Task<string> DeleteInvestor(ClsInvestor investor);
                
    }
}
