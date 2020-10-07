namespace CashRolls.Web.Profiles
{
    using System.Linq;

    using AutoMapper;

    using CashRolls.Data.Models;
    using CashRolls.Web.Models.ViewModels;

    public class RollsProfile : Profile
    {
        public RollsProfile()
        {
            CreateMap<Roll, RollsParticipantsParticipant>();

            CreateMap<Roll, RollsHistoryRoll>()
                .ForMember(destination => destination.CreatedOn, x => x.MapFrom(source => source.CreatedOn.ToShortDateString()))
                .ForMember(destination => destination.EntryFee, x => x.MapFrom(source => source.EntryFee.ToString("0")))
                .ForMember(destination => destination.ParticipantsCount, x => x.MapFrom(source => source.RollsUsers.Where(ru => ru.IsPending == false).Count()))
                .ForMember(destination => destination.Winner, x => x.MapFrom(source => source.Winner.UserName))
                .ForMember(destination => destination.Reward, x => x.MapFrom(source => (source.ParticipantsCount * source.EntryFee * ((100 - source.CutPercent) / 100)).ToString("0.00")));

            CreateMap<Roll, RollsStatisticsRoll>()
                .ForMember(destination => destination.Currency, x => x.MapFrom(source => $"{source.Currency.Name}({source.Currency.Symbol})"))
                .ForMember(destination => destination.Tuition, x => x.MapFrom(source => $"{source.EntryFee:0}{source.Currency.Symbol}"))
                .ForMember(destination => destination.EntryPriceTaxPercent, x => x.MapFrom(source => $"{source.CutPercent:0.00}%"))
                .ForMember(destination => destination.MaxSum, x => x.MapFrom(source => source.MaxSum.GetValueOrDefault().ToString("0")))
                .ForMember(destination => destination.Winner, x => x.MapFrom(source => source.Winner.UserName));

            CreateMap<Roll, HomeDashboardRoll>()
                .ForMember(destination => destination.CreatedOn, x => x.MapFrom(source => source.CreatedOn.ToShortDateString()))
                .ForMember(destination => destination.EntryPrice, x => x.MapFrom(source => source.EntryFee.ToString("0")))
                .ForMember(destination => destination.EntryPriceStripe, x => x.MapFrom(source => (source.EntryFee * 100).ToString("0")))
                .ForMember(destination => destination.ParticipantsCount, x => x.MapFrom(source => source.RollsUsers.Where(ru => ru.IsPending == false).Count().ToString()))
                .ForMember(destination => destination.Reward, x => x.MapFrom(source => (source.EntryFee * source.RollsUsers.Count(rollUser => rollUser.IsPending == false) * (1 - source.CutPercent / 100)).ToString("0.00")))
                .ForMember(destination => destination.CreatedOn, x => x.MapFrom(source => source.CreatedOn.ToShortDateString()))
                .ForMember(destination => destination.EntryPriceTaxPercent, x => x.MapFrom(source => source.CutPercent.ToString("0.00")))
                .ForMember(destination => destination.ParticipantIds, x => x.MapFrom(source => source.RollsUsers.Where(ru => ru.DeletedOn == null).Select(ru => ru.Id)));
        }
    }
}
