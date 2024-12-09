using AutoMapper;
using POD.Common.Database.Models;
using POD.Functions.Payment.Common.Data.Models;

namespace POD.Functions.Payment.Schedule.Services.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<StoreSaleOrderDetail, SaleTransactionDetail>()
                .ForMember(cstd => cstd.StoreSaleOrderDetailsId, opt => opt.MapFrom(src => src.Id))
                .ForMember(cstd => cstd.Shipping, opt => opt.MapFrom(src => src.ShippingPrice))
                .ForMember(cstd => cstd.Price, opt => opt.MapFrom(src => src.ItemPrice))
                .ForMember(cstd => cstd.Discount, opt => opt.MapFrom(src => src.Discount))
                // .ForMember(cstd => cstd.Tax, opt => opt.MapFrom(src => src.Tax)) // TODO Implement
                .ForMember(cstd => cstd.Total, opt => opt.MapFrom(src => src.Total));
        }
    }
}