using Common.Application;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using UserModule.Core.Services;
using UserModules.Core.Commands.Users.Edit;

namespace DigiLearn.Web.Pages.Profile
{
    [BindProperties]
    public class Edit : BaseRazor
    {
        private readonly IUserFacade _userFacade;

        public Edit(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Family { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public async Task OnGet()
        {
            var user = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
            if (user != null)
            {
                Name = user.Name;
                Family = user.Family;
                Email = user.Email;
            }
        }
        public async Task<IActionResult> OnPost()
        {
            var reslut = await _userFacade.EditUserProfile(new EditUserCommand()
            {
                Name = Name,
                Family = Family,
                Email = Email,
                UserId = User.GetUserId()
            });

            return RedirectAndShowAlert(reslut, RedirectToPage("index"));
        }
    }
}
