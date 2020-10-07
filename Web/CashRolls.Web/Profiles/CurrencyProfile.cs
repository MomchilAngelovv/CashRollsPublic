namespace CashRolls.Web.Profiles
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using CashRolls.Data.Models;

    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<Currency, SelectListItem>()
                .ForMember(destination => destination.Value, x => x.MapFrom(source => source.Id))
                .ForMember(destination => destination.Text, x => x.MapFrom(source => source.Name));
        }
    }
}
