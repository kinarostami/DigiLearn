using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using UserModule.Core.Services;
using UserModules.Core.Commands.Users.ChangePassword;

namespace DigiLearn.Web.Pages.Profile
{
    [BindProperties]
    public class ChangePasswordModel : BaseRazor
    {
        private readonly IUserFacade _userFacade;

        public ChangePasswordModel(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }
        [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Compare("NewPassword",ErrorMessage = "کلمه های عبور یکسان نیستند")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        public async Task OnGet()
        {
            var user = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
            if (user != null)
            {
                CurrentPassword = user.Password;
            }
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _userFacade.ChangePassword(new ChangeUserPasswordCommand()
            {
                CurrentPassword = CurrentPassword,
                NewPassword = NewPassword,
                UserId = User.GetUserId()
            });

            return RedirectAndShowAlert(result, RedirectToPage("index"));
        }
    }
}
