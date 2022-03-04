using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Car, CarForListDto>()
            .ForMember(x => x.carInsuranceExpirationDate, y => y.MapFrom(z => z.CarInsurance.ExpirationDate))
            .ForMember(x => x.UserName, y => y.MapFrom(z => z.AppUser.DisplayName))
            .ForMember(x => x.TechnicalExaminationExpirationDate, y => y.MapFrom(z => z.TechnicalExamination.TechnicalExaminationExpirationDate))
            .ForMember(x => x.NextServiceMeterStatus, y => y.MapFrom(z => z.Service.NextServiceMeterStatus))
            .ForMember(x => x.ServiceExpirationDate, y => y.MapFrom(z => z.Service.ServiceExpirationDate));


            CreateMap<Car, CarDto>()
            .ForMember(x => x.Insurance, y => y.MapFrom(z => z.CarInsurance))
            .ForMember(x => x.UserName, y => y.MapFrom(z => z.AppUser.UserName))
            .ForMember(x => x.TechnicalExaminationExpirationDate, y => y.MapFrom(z => z.TechnicalExamination.TechnicalExaminationExpirationDate))
            .ReverseMap();

            CreateMap<Service, ServiceDto>().ReverseMap();

            CreateMap<Tires, TiresForListDto>()
            .ForMember(x => x.Car, y=> y.MapFrom(z=> $"{z.Car.Brand} {z.Car.Model}"))
            .ReverseMap();
        
            CreateMap<Tires, TiresDto>().ReverseMap();

            CreateMap<CarInsurance, InsuranceForListDto>()
            .ForMember(x => x.Car, y=> y.MapFrom(z=> $"{z.Car.Brand} {z.Car.Model}"))
            .ReverseMap();

            CreateMap<CarInsurance, InsuranceDto>().ReverseMap();

            CreateMap<LogDto, Log>().ReverseMap();
            CreateMap<Field, FieldDto>().ReverseMap();
            CreateMap<Field, FieldForListDto>().ReverseMap();

          
           
            
        }
    }
}