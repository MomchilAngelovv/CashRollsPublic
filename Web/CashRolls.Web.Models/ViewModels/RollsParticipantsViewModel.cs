namespace CashRolls.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class RollsParticipantsViewModel
    {
        public string RollId { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<RollsParticipantsParticipant> Participants { get; set; }
    }
}
