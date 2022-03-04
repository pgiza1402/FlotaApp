using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InsuranceController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InsuranceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InsuranceForListDto>>> GetInsurancesAsync()
        {
            var insurances = await _unitOfWork.InsuranceRepository.GetInsurancesAsync();

            var insurancesToReturn = _mapper.Map<IReadOnlyList<CarInsurance>, IReadOnlyList<InsuranceForListDto>>(insurances);

            return Ok(insurancesToReturn);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("{id}")]
        public async Task<ActionResult<InsuranceForListDto>> GetInsuranceById(int id)
        {
            var insurance = await _unitOfWork.InsuranceRepository.GetInsuranceById(id);

            if (insurance == null) return BadRequest("Brak polisy o wybranym id");

            var insuranceToReturn = new InsuranceForListDto();

            _mapper.Map(insurance, insuranceToReturn);

            return Ok(insuranceToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InsuranceDto>> UpdateInsurance(InsuranceDto insuranceDto, int id)
        {
            var insurance = await _unitOfWork.InsuranceRepository.GetInsuranceById(id);

            if (insurance == null) return BadRequest("Brak polisy o wybranym id");

            _mapper.Map(insuranceDto, insurance);

            _unitOfWork.InsuranceRepository.Update(insurance);

            if (await _unitOfWork.Complete())
            {
                return insuranceDto;
            }
            else
            {
                return BadRequest("Nie udało się zaktualizować polisy");
            }
        }
    }
}




