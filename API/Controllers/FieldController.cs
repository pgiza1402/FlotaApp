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
    public class FieldController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FieldController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<FieldForListDto>>> GetFieldsAsync()
        {

            var fields = await _unitOfWork.FieldRepository.GetFieldsAsync();

            var fieldsToReturn = _mapper.Map<IReadOnlyList<Field>, IReadOnlyList<FieldForListDto>>(fields);

            return Ok(fieldsToReturn);

        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldDto>> GetFieldByIdAsync(int id)
        {

            var field = await _unitOfWork.FieldRepository.GetFieldByIdAsync(id);

            if (field == null) return BadRequest("Brak pozycji o wybranym id");

            var fieldToReturn = new FieldDto();

            _mapper.Map(field, fieldToReturn);

            return Ok(fieldToReturn);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<ActionResult<FieldDto>> AddFieldAsync(FieldDto fieldDto)
        {

            var field = new Field();

            _mapper.Map(fieldDto, field);

            await _unitOfWork.FieldRepository.AddFieldAsync(field);

            return Ok(fieldDto);

        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("{id}")]
        public async Task<ActionResult<FieldDto>> UpdateField(FieldDto fieldDto, int id)
        {
            var field = await _unitOfWork.FieldRepository.GetFieldByIdAsync(id);

            if(field == null) return BadRequest("Brak pozycji o wybranym id");

            _mapper.Map(fieldDto , field);

            _unitOfWork.FieldRepository.Update(field);

            if(await _unitOfWork.Complete())
            {
                return Ok(fieldDto);
            }
            else{

                return BadRequest("Nie udało się zaktualizować pola");
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete ("{id}")]
        public async Task<ActionResult> RemoveField(int id){

            var field = await _unitOfWork.FieldRepository.GetFieldByIdAsync(id);

            if(field == null) return BadRequest("Brak pozycji o wybranym id");

            _unitOfWork.FieldRepository.Delete(field);

            if(await _unitOfWork.Complete())
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Nie udało się usunąć pola");
            }
        }
        
    }
}