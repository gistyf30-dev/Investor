using Investor.Models;
using Investor.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;


namespace Investor.Controllers
{
    public class InvestorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IInvestorRepository _investorRepository;
        public InvestorController(ILogger<HomeController> logger, IInvestorRepository investorRepository)
        {
            _logger = logger;
            _investorRepository = investorRepository;
        }
        public IActionResult Investor()
        {
            return View();
        }
        public async Task<string> SelectAllInvestors()
        {
            var investors = await _investorRepository.GetInvestors();
            return investors;
        }
        public async Task<string> SingleInvestor([FromBody] ClsInvestor ObjClsInvestor)
        {
            var investor = await _investorRepository.GetInvestorById(ObjClsInvestor.InvestorId);
            return investor;
        }
        
        public async Task<string> AddInvestors([FromBody] InvestorRequest ObjClsInvestor)
        {
            var investor = await _investorRepository.AddInvestor(ObjClsInvestor);
            return investor;
        }

        public async Task<string> AddFundForInvestor([FromBody] InvestorFund ObjClsInvestor)
        {
            var fund = await _investorRepository.AddFundToInvestor(ObjClsInvestor);
            return fund;
        }
      
        public async Task<string> DeleteInvestor([FromBody] ClsInvestor ObjClsInvestor)
        {
            var objResult = await _investorRepository.DeleteInvestor(ObjClsInvestor);
            return objResult;
        }
        public async Task<string> getFundList()
        {
            var fundList = await _investorRepository.GetFundList();
            return fundList;
        }
    }
}
