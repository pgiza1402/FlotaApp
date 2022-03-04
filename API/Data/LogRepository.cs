using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LogRepository : ILogRepository
    {
        private readonly DataContext _context;
        public LogRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Log>> AddCarLogAsync(string user, Car car, CarDto carDto)
        {
            List<Log> logs = new List<Log>();
            if (carDto.UserName != car.AppUser?.UserName)
            {
                logs.Add(new Log
                {
                    User = user,
                    Position = $"{car.Brand} {car.Model} - {car.RegistrationNumber}",
                    Field = "Uzytkownik",
                    OldValue = car.AppUser?.UserName,
                    NewValue = carDto.UserName
                });
            }
            if (carDto.MeterStatus != car.MeterStatus.ToString())
            {
                logs.Add(new Log
                {
                    User = user,
                    Position = $"{car.Brand} {car.Model} - {car.RegistrationNumber}",
                    Field = "Stan Licznika",
                    OldValue = car.MeterStatus.ToString(),
                    NewValue = carDto.MeterStatus
                });
            }
            if (carDto.Insurance.ExpirationDate != car.CarInsurance.ExpirationDate)
            {
                logs.Add(new Log
                {
                    User = user,
                    Position = $"{car.Brand} {car.Model} - {car.RegistrationNumber}",
                    Field = "Ważność ubezpieczenia",
                    OldValue = car.CarInsurance.ExpirationDate.ToString("dd-MM-yyyy"),
                    NewValue = carDto.Insurance.ExpirationDate.ToString("dd-MM-yyyy")
                });
            }
            if (carDto.TechnicalExaminationExpirationDate != car.TechnicalExamination.TechnicalExaminationExpirationDate)
            {

                logs.Add(new Log
                {
                    User = user,
                    Position = $"{car.Brand} {car.Model} - {car.RegistrationNumber}",
                    Field = "Termin Badania Technicznego",
                    OldValue = car.TechnicalExamination.TechnicalExaminationExpirationDate.ToString("dd-MM-yyyy"),
                    NewValue = carDto.TechnicalExaminationExpirationDate.ToString("dd-MM-yyyy")
                });
            }
            if (carDto.Service.ServiceExpirationDate != car.Service.ServiceExpirationDate)
            {
                logs.Add(new Log
                {
                    User = user,
                    Position = $"{car.Brand} {car.Model} - {car.RegistrationNumber}",
                    Field = "Termin Serwisu",
                    OldValue = car.Service.ServiceExpirationDate.ToString("dd-MM-yyyy"),
                    NewValue = carDto.Service.ServiceExpirationDate.ToString("dd-MM-yyyy")
                });

            }
            if (carDto.Service.NextServiceMeterStatus != car.Service.NextServiceMeterStatus)
            {
                logs.Add(new Log
                {
                    User = user,
                    Position = $"{car.Brand} {car.Model} - {car.RegistrationNumber}",
                    Field = "Stan licznika nast. serwisu",
                    OldValue = car.Service.NextServiceMeterStatus.ToString(),
                    NewValue = carDto.Service.NextServiceMeterStatus.ToString()
                });

            }
            if (logs != null && logs.Count > 0)
            {
                foreach (var log in logs)
                {
                    await _context.Logs.AddAsync(log);
                }
                await _context.SaveChangesAsync();
            }

            return logs;
        }

        public async Task<IReadOnlyList<Log>> AddUserLogAsync(string user, AppUser appUser, UserDtoUpdate userDtoUpdate)
        {
            List<Log> logs = new List<Log>();
            if (userDtoUpdate.Email != appUser.Email)
            {
                logs.Add(new Log
                {
                    User = user,
                    Position = $"{appUser.DisplayName}",
                    Field = "Adres e-mail",
                    OldValue = appUser.Email,
                    NewValue = userDtoUpdate.Email
                });
            }
            if (userDtoUpdate.PhoneNumber != appUser.PhoneNumber)
            {
                logs.Add(new Log
                {
                    User = user,
                    Position = $"{appUser.DisplayName}",
                    Field = "Numer telefonu",
                    OldValue = appUser.PhoneNumber,
                    NewValue = userDtoUpdate.PhoneNumber
                });
            }
            if (logs != null && logs.Count > 0)
            {
                foreach (var log in logs)
                {
                    await _context.Logs.AddAsync(log);
                }
                await _context.SaveChangesAsync();
            }

            return logs;
        }

        public async Task<IReadOnlyList<Log>> GetLogsAsync()
        {
            return await _context.Logs.OrderByDescending(x => x.Id).ToListAsync();
        }

    }
}