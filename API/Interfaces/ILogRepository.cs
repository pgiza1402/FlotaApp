using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface ILogRepository
    {
        Task<IReadOnlyList<Log>> AddCarLogAsync(string user, Car car, CarDto carToAddDto);
        Task<IReadOnlyList<Log>> AddUserLogAsync(string user, AppUser appUser, UserDtoUpdate userDtoUpdate);
        Task<IReadOnlyList<Log>> GetLogsAsync();
    
    }
}