using Investor.Models;
using Investor.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Investor.Services
{
    public class InvestorRespository: IInvestorRepository
    {
        private readonly string UrlAPI = "https://localhost:7280/api/";

        async Task<string> IInvestorRepository.AddInvestor(InvestorRequest Investor)
        {
            ClsInvestor objInvestor = new ClsInvestor();
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(Investor), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(UrlAPI + "Investors", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    objInvestor.StatusDetail = new StatusDetails();
                    objInvestor.StatusDetail.StatusCode = 200;

                    var data = JsonConvert.SerializeObject(objInvestor, Formatting.Indented);
                    return data;  
                }
                else
                {
                    //ToDo - display an error to the user
                    objInvestor.StatusDetail = new StatusDetails();
                    objInvestor.StatusDetail.StatusCode = 400;
                    objInvestor.StatusDetail.Status = response.ReasonPhrase;
                    var investorsJson = JsonConvert.SerializeObject(objInvestor, Formatting.Indented);
                    return investorsJson;
                }
            };
        }

        async Task<string> IInvestorRepository.GetInvestors()
        {
            HttpClient client = new HttpClient();
            IEnumerable<ClsInvestor> investors = null;
            ClsInvestor objlst = new ClsInvestor();

            try
            {
                var response = await client.GetAsync(UrlAPI + "investors");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    investors = JsonConvert.DeserializeObject<IEnumerable<ClsInvestor>>(responseData);

                    foreach (var investor in investors)
                    {
                        // Create and assign the comma-separated list of FundIds
                        investor.FundsInvestedIn = string.Join(", ", investor.Funds.Select(f => f.FundName.ToString()));
                        investor.StatusDetail = new StatusDetails();
                        investor.StatusDetail.StatusCode = 200;

                    }

                    var investorsJson = JsonConvert.SerializeObject(investors, Formatting.Indented);
                    return investorsJson;
                }
                else
                {
                    objlst.StatusDetail = new StatusDetails();
                    objlst.StatusDetail.StatusCode = 400;
                    objlst.StatusDetail.Status = response.ReasonPhrase;
                    var investorsJson = JsonConvert.SerializeObject(objlst, Formatting.Indented);
                    return investorsJson;
                }
            }
            catch (Exception ex)
            {
                objlst.StatusDetail = new StatusDetails();
                objlst.StatusDetail.StatusCode = 400;
                objlst.StatusDetail.Status = ex.Message.ToString();
                var investorsJson = JsonConvert.SerializeObject(objlst, Formatting.Indented);
                return investorsJson;
            }
           // return String.Empty;
        }

        async Task<string> IInvestorRepository.GetInvestorById(int investorId)
        {
            HttpClient client = new HttpClient();
            ClsInvestor investor = null;
            ClsLstInvestor investorList = null;

            try
            {                
                var response = await client.GetAsync(UrlAPI + $"Investors/{investorId}");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    investor = JsonConvert.DeserializeObject<ClsInvestor>(responseData);

                    investorList = new ClsLstInvestor();
                    investorList.ObjClsLInvestor = new List<ClsInvestor>();
                    investorList.ObjClsLInvestor.Add(investor);
                    foreach (var invest in investorList.ObjClsLInvestor)
                    {
                        investor.FundsInvestedIn = string.Join(", ", invest.Funds.Select(f => f.FundName.ToString()));
                        investor.StatusDetail = new StatusDetails();
                        investor.StatusDetail.StatusCode = 200;
                    }
                    var investorsJson = JsonConvert.SerializeObject(investorList, Formatting.Indented);
                    return investorsJson;
                }
                else
                {
                    investorList = new ClsLstInvestor();
                    investorList.StatusDetail = new StatusDetails();
                    investorList.StatusDetail.StatusCode = 400;
                    investorList.StatusDetail.Status = response.ReasonPhrase;
                    var investorsJson = JsonConvert.SerializeObject(investorList, Formatting.Indented);
                    return investorsJson;
                }
            }
            catch (Exception ex)
            {
                investorList = new ClsLstInvestor();
                investorList.StatusDetail = new StatusDetails();
                investorList.StatusDetail.StatusCode = 400;
                investorList.StatusDetail.Status = ex.Message.ToString();
                var investorsJson = JsonConvert.SerializeObject(investorList, Formatting.Indented);
                return investorsJson;
            }
          //  return String.Empty;
        }

        async Task<string> IInvestorRepository.AddFundToInvestor(InvestorFund fund)
        {
            Fund objFund = new Fund();
            using (var client = new HttpClient())
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(fund), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(UrlAPI + "Investors/AddFund", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        objFund.StatusDetail = new StatusDetails();
                        objFund.StatusDetail.StatusCode = 200;

                        var data = JsonConvert.SerializeObject(objFund, Formatting.Indented);
                        return data;
                    }
                    else
                    {
                        objFund.StatusDetail = new StatusDetails();
                        objFund.StatusDetail.StatusCode = 400;
                        objFund.StatusDetail.Status = response.ReasonPhrase;
                        var investorsJson = JsonConvert.SerializeObject(objFund, Formatting.Indented);
                        return investorsJson;
                    }
                }
                catch(Exception ex)
                {
                    objFund.StatusDetail = new StatusDetails();
                    objFund.StatusDetail.StatusCode = 400;
                    objFund.StatusDetail.Status = ex.Message.ToString();
                    var investorsJson = JsonConvert.SerializeObject(objFund, Formatting.Indented);
                    return investorsJson;
                }
            };
        }

        async Task<string> IInvestorRepository.DeleteInvestor(ClsInvestor ObjClsInvestor)
        {
            ClsLstInvestor ObjLst = new ClsLstInvestor();

            using (var client = new HttpClient())
            {
                try
                {                    
                    var response = await client.DeleteAsync(UrlAPI + $"Investors/{ObjClsInvestor.InvestorId}");

                    if (response.IsSuccessStatusCode)
                    {
                        ObjLst.StatusDetail = new StatusDetails();
                        ObjLst.StatusDetail.Status = "Investor Deleted Successfully";
                        ObjLst.StatusDetail.StatusCode = 200;
                        var investorsJson = JsonConvert.SerializeObject(ObjLst, Formatting.Indented);
                        return investorsJson;
                    }
                    else
                    {


                        ObjLst.StatusDetail = new StatusDetails();
                        ObjLst.StatusDetail.StatusCode = 400;
                        ObjLst.StatusDetail.Status = response.ReasonPhrase;
                        var investorsJson = JsonConvert.SerializeObject(ObjLst, Formatting.Indented);
                        return investorsJson;
                    }
                }
                catch(Exception ex)
                {
                    ObjLst.StatusDetail = new StatusDetails();
                    ObjLst.StatusDetail.StatusCode = 400;
                    ObjLst.StatusDetail.Status = ex.Message.ToString();
                    var investorsJson = JsonConvert.SerializeObject(ObjLst, Formatting.Indented);
                    return investorsJson;
                }
            };
   
        }

        async Task<string> IInvestorRepository.GetFundList()
        {
            HttpClient client = new HttpClient();
            IEnumerable<Fund> fundList = null;
            ClsInvestor investorList = null;
            try
            {
                var response = await client.GetAsync(UrlAPI + "Investors/getFundList");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    fundList = JsonConvert.DeserializeObject<IEnumerable<Fund>>(responseData);
                    foreach (var funds in fundList)
                    {

                        funds.StatusDetail = new StatusDetails();
                        funds.StatusDetail.StatusCode = 200;
                    }

                    var fundJson = JsonConvert.SerializeObject(fundList, Formatting.Indented);
                    return fundJson;
                }
                else
                {
                    investorList = new ClsInvestor();
                    investorList.StatusDetail = new StatusDetails();
                    investorList.StatusDetail.StatusCode = 400;
                    investorList.StatusDetail.Status = response.ReasonPhrase;
                    var investorsJson = JsonConvert.SerializeObject(investorList, Formatting.Indented);
                    return investorsJson;
                }
            }
            catch (Exception ex)
            {
                investorList = new ClsInvestor();
                investorList.StatusDetail = new StatusDetails();
                investorList.StatusDetail.StatusCode = 400;
                investorList.StatusDetail.Status = ex.Message.ToString();
                var investorsJson = JsonConvert.SerializeObject(investorList, Formatting.Indented);
                return investorsJson;
            }
            return String.Empty;
        }
    }
}
