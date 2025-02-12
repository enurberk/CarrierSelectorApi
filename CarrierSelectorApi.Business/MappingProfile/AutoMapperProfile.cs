using AutoMapper;
using CarrierSelectorApi.Entities.DTOs.CarrierConfigurationDTOs;
using CarrierSelectorApi.Entities.DTOs.CarrierDTOs;
using CarrierSelectorApi.Entities.DTOs.OrderDTOs;
using CarrierSelectorApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarrierSelectorApi.Business.MappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Carrier, CarrierDto>().ReverseMap();
            CreateMap<Carrier, CarrierCreateDto>().ReverseMap();
            CreateMap<Carrier, CarrierUpdateDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();

            CreateMap<CarrierConfiguration, CarrierConfigurationDto>().ReverseMap();
            CreateMap<CarrierConfiguration, CarrierConfigurationCreateDto>().ReverseMap();
            CreateMap<CarrierConfiguration, CarrierConfigurationUpdateDto>().ReverseMap();   
        }
    }
}
