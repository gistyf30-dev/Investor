using AutoMapper;
using InvestorsAPI.Entities;
using InvestorsAPI.Entities.Dtos;
using InvestorsAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InvestorsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestorsController : ControllerBase
    {
        private IInvestorFundsRepository _investorsRepository;
        private readonly IMapper _mapper;

        public InvestorsController(IInvestorFundsRepository investorsRepository, IMapper mapper)
        {
            _investorsRepository = investorsRepository;
            _mapper = mapper;
        }

        [HttpPost("AddFund")]
        [ProducesResponseType(201, Type = typeof(InvestorFund))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult AddFundForInvestor([FromBody] InvestorFund investorFund)
        {
            if (investorFund == null)
                return BadRequest(ModelState);

            if (!_investorsRepository.CreateInvestorFund(investorFund))
            {
                ModelState.AddModelError("", $"Something went wrong saving the investor fund");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetInvestorById", new { investorId = investorFund.InvestorId }, investorFund);

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Investor))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateInvestor([FromBody] Investor investorToCreate)
        {
            if (investorToCreate == null)
                return BadRequest(ModelState);

            if (!_investorsRepository.CreateInvestor(investorToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving the investor " +
                                            $"{investorToCreate.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetInvestorById", new { investorId = investorToCreate.InvestorId }, investorToCreate);
        }

        //api/investors
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Investor>))]
        [ProducesResponseType(400)]
        public IActionResult GetInvestors()
        {
            var investors = _investorsRepository.GetAllInvestors();

            if (investors == null)
            {
                return NotFound();
            }

            var investorDto = _mapper.Map<IEnumerable<InvestorDto>>(investors);

            return Ok(investorDto);
        }

        [HttpGet("getFundList")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Fund>))]
        [ProducesResponseType(400)]
        public IActionResult getFundList()
        {
            var fundlist = _investorsRepository.getAllFunds();

            if (fundlist == null)
            {
                return NotFound();
            }

            var investorDto = _mapper.Map<IEnumerable<Fund>>(fundlist);

            return Ok(investorDto);

        }

        [HttpGet("{investorid}", Name= "GetInvestorById")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Investor>))]
        [ProducesResponseType(400)]
        public IActionResult GetInvestorById([FromRoute] int investorId)
        {
            var investor = _investorsRepository.GetInvestorById(investorId);

            if (investor == null)
            {
                return NotFound();
            }

            var investorDto = _mapper.Map<InvestorDto>(investor);

            return Ok(investorDto);

        }

        [HttpDelete("{investorId}")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult DeleteInvestor([FromRoute] int investorId)
        {
            if (!_investorsRepository.InvestorExists(investorId))
                return NotFound();

            var investorToDelete = _investorsRepository.GetInvestorById(investorId);

            if (_investorsRepository.GetFundsByInvestorId(investorId).Count() > 0)
            {
                ModelState.AddModelError("", $"Investor {investorToDelete.Name} " +
                                              $"cannot be deleted because he/she has funds invested");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_investorsRepository.DeleteInvestor(investorToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong deleting " +
                                            $"{investorToDelete.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
