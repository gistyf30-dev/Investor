namespace Investor.Models
{
    public class ClsCommon
    {

    }
    public class ClsLstInvestor
    {
        public IEnumerable<ClsInvestor>? ObjClsInvestor { get; set; }
        public List<ClsInvestor>? ObjClsLInvestor { get; set; }
        public List<ClsCommonDropdown>? ObjClsDropDown { get; set; }
        public StatusDetails? StatusDetail { get; set; }
    }
    public class ClsCommonDropdown
    {
        public string? Id { get; set; }
        public string? Value { get; set; }
    }
    public class StatusDetails
    {
        public string? Status { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
    public class ClsInvestor
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? FundsInvestedIn { get; set; }
        public int InvestorId { get; set; }
        public string? UserName { get; set; }
        public string? Mode { get; set; }
        public string? FundId { get; set; }
        public List<FundDto>? Funds { get; set; }

        public StatusDetails? StatusDetail { get; set; }
    }
    public class InvestorRequest
    {

        public int InvestorId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }

        public ICollection<InvestorFund>? InvestorFunds { get; set; }
    }

    public class InvestorFund
    {
  
        public int InvestorId { get; set; }
        public InvestorRequest? Investor { get; set; }
   
        public int FundId { get; set; }
        public Fund? Fund { get; set; }
    }
    public class Fund
    {
        
        public int FundId { get; set; }
        public string? FundName { get; set; }
        public List<InvestorFund>? InvestorFunds { get; set; }
        public StatusDetails? StatusDetail { get; set; }
    }
    public class FundDto
    {
        public int? FundId { get; set; }
        public string? FundName { get; set; }
    }
}
