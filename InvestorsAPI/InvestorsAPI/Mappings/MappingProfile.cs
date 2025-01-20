using AutoMapper;
using InvestorsAPI.Entities;
using InvestorsAPI.Entities.Dtos;
using InvestorsAPI.Services;

namespace InvestorsAPI.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {

            //Creates a mapping between the investor entity and investorDto
            //The forMember tells automapper to map funds collection in investors to the funds property in investorsDto
            CreateMap<Investor, InvestorDto>()
                .ForMember(dest => dest.Funds, opt => opt.MapFrom(src => src.InvestorFunds.Select(sc => sc.Fund)));

            CreateMap<Fund, FundDto>();
        } 
    }
}
