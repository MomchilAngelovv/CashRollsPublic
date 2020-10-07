namespace CashRolls.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using CashRolls.Services;
    using CashRolls.Web.Common;
    using CashRolls.Web.Models.ViewModels;
    using CashRolls.Web.Models.InputModels;

    public class RollsController : Controller
    {

        private readonly IRollsService rollsService;

        public RollsController(
            IRollsService rollsService)
        {
            this.rollsService = rollsService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public IActionResult Create()
        {
            return this.View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Join(string pendingJoinId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await rollsService.ConfirmJoinAsync(pendingJoinId, userId);

            return Redirect("/Home/Dashboard");
        }
        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Close()
        {
            await rollsService.CloseAsync();
            return Redirect("/Home/Dashboard");
        }
        [HttpGet]
        public IActionResult Winners()
        {
            var rolls = rollsService
                .GetClosedRolls<RollsHistoryRoll>()
                .OrderByDescending(roll => roll.Winner == null)
                .ThenByDescending(roll => roll.CreatedOn)
                .ToList();

            var viewModel = new RollsHistoryViewModel
            {
                Rolls = rolls
            };

            return this.View(viewModel);
        }
        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> RollWinner(RollsRollWinnerInputModel inputModel)
        {
            await rollsService.RollWinnerAsync(inputModel.Id);
            return Redirect("/Rolls/Winners");
        }
        [HttpGet]
        [Authorize(Roles = Roles.Administrator)]
        public IActionResult Statistics()
        {
            var rolls = rollsService
                .GetAll<RollsStatisticsRoll>()
                .OrderByDescending(roll => roll.IsActive)
                .ToList();

            var viewModel = new RollsStatisticsViewModel
            {
                Rolls = rolls
            };

            return this.View(viewModel);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Participants(RollsParticipantsInputModel inputModel)
        {
            var participants = this.rollsService.GetParticipantsByPage<RollsParticipantsParticipant>(inputModel.Id, inputModel.Page);

            var viewModel = new RollsParticipantsViewModel
            {
                RollId = inputModel.Id,
                CurrentPage = inputModel.Page,
                Participants = participants
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Create(RollsCreateInputModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return this.View(inputModel);
            }

            await rollsService.CreateAsync(inputModel.EntryPrice, inputModel.CurrencyId, inputModel.EntryPriceTaxPercent, inputModel.MaxSum);

            return Redirect("/Home/Dashboard");
        }
    }
}
