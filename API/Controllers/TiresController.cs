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
    public class TiresController : BaseApiController
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        public TiresController(IUnitOfWork unitofWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitofWork = unitofWork;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TiresForListDto>>> GetTiresAsync()
        {
            var tires = await _unitofWork.TiresRepository.GetTiresAsync();

            var tiresToReturn = _mapper.Map<IReadOnlyList<Tires>, IReadOnlyList<TiresForListDto>>(tires);

            return Ok(tiresToReturn);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet ("{id}")]
        public async Task<ActionResult<TiresDto>> GetTiresByIdAsync(int id){
            var tires = await _unitofWork.TiresRepository.GetTiresByIdAsync(id);

            if(tires == null)
            {
                return BadRequest("Brak opon o wybranym id");
            }

            var tiresToReturn = new TiresDto();

            _mapper.Map(tires, tiresToReturn);

            return Ok(tiresToReturn);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<ActionResult<TiresDto>> AddTiresAsync(TiresDto tiresDto)
        {
            var tires = new Tires();
            _mapper.Map(tiresDto, tires);
            await _unitofWork.TiresRepository.AddTiresAsync(tires);

            return Ok(tiresDto);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("{id}")]
        public async Task<ActionResult<TiresDto>> UpdateTiresAsync(TiresDto tiresToUpdateDto, int id)
        {


            var tires = await _unitofWork.TiresRepository.GetTiresByIdAsync(id);

            if (tires == null)
            {
                return BadRequest("Brak opon o wybranym id");
            }
            _mapper.Map(tiresToUpdateDto, tires);

            _unitofWork.TiresRepository.Update(tires);

            if (await _unitofWork.Complete())
            {
                return Ok(tiresToUpdateDto);
            }
            else
            {
                return BadRequest("Nie udało się zaktualizować opon");
            }


        }
    }

}