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
    public class LogController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public LogController(IMapper mapper, IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<LogDto>>> GetLogsAsync(){

        var logs = await _unitOfWork.LogRepository.GetLogsAsync();

        var logsToReturn = _mapper.Map<IReadOnlyList<Log>, IReadOnlyList<LogDto>>(logs);

        return Ok(logsToReturn);
        }

        
    }
}