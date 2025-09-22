using Common.Application;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using TIcketModules.Core.DTOs.Tickets;
using TIcketModules.Core.Services;
using TIcketModules.Data.Entities;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile.Ticket
{
    public class ShowModel : BaseRazor
    {
        private readonly ITicketService _ticketService;
        private readonly IUserFacade _userFacade;

        public ShowModel(ITicketService ticketService, IUserFacade userFacade)
        {
            _ticketService = ticketService;
            _userFacade = userFacade;
        }
        public TicketDto Ticket { get; set; }

        [BindProperty]
        [Display(Name = "متن تیکت")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Text { get; set; }

        public async Task<IActionResult> OnGet(Guid ticketId)
        {
            var ticket = await _ticketService.GetTicket(ticketId);
            if (ticket == null || ticket.UserId != User.GetUserId())
                return RedirectToPage("index");

            Ticket = ticket;
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid ticketId)
        {
            var user = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
            var message = new SendTicketMessageCommand()
            {
                UserId = User.GetUserId(),
                TicketId = ticketId,
                Text = Text,
                OwnerFullName = $"{user.Name} {user.Family}"
            };
            var result = await _ticketService.SendMessageInTicket(message);
            return RedirectAndShowAlert(result, RedirectToPage("show", new { ticketId }));
        }

        public async Task<IActionResult> OnPostCloseTicket(Guid ticketId)
        {
            return await AjaxTryCatch(async () =>
            {
                var ticket = await _ticketService.GetTicket(ticketId);
                if (ticket == null || ticket.UserId != User.GetUserId())
                    return OperationResult.Error("تیکت یافت نشد");

                return await _ticketService.CloseTicket(ticketId); 
            });
        }
    }
}
