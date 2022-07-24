using System;
using AutoMapper;
using Order.API;

namespace Orders.API.Profiles
{
    public class PurchasesProfile : Profile
    {
        public PurchasesProfile()
        {
            CreateMap<Order.API.Models.PurchaseForCreationDto, Order.API.Entities.Purchase>()
            .ForMember(
                dest => dest.PurchaseId,
                opts => opts.Ignore()
            );
        }
        
    }
}