namespace CashRolls.Web.Profiles
{
    using AutoMapper;

    using CashRolls.Data.Models;
    using CashRolls.Web.Models.ViewModels;

    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            this.CreateMap<User, RollsParticipantsParticipant>();

            this.CreateMap<User, UsersAllUser>();

            this.CreateMap<ContactMessage, UsersProfileContactMessage>();
        }
    }
}
