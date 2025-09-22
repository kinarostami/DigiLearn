using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TIcketModules.Core.DTOs.Tickets;
using TIcketModules.Core.Services;
using UserModule.Core.Services;
using ZstdSharp.Unsafe;

namespace DigiLearn.Web.Pages.Profile.Ticket
{
    [BindProperties]
    public class AddModel : BaseRazor
    {
        private readonly ITicketService _ticketService;
        private readonly IUserFacade _userFacade;

        public AddModel(ITicketService ticketService, IUserFacade userFacade)
        {
            _ticketService = ticketService;
            _userFacade = userFacade;
        }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "متن تیکت")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
            var command = new CreateTicketCommand()
            {
                UserId = user.Id,
                PhoneNumber = user.PhoneNumber,
                OwnerFullName = (user.Name,user.Family).ToString(),
                Title = Title,
                Text = Text
            };
            var result = await _ticketService.CreateTicket(command);
            return RedirectAndShowAlert(result,RedirectToPage("index"));
        }
    }
}
