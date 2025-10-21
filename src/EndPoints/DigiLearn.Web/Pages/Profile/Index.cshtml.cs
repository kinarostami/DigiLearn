using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Services;
using UserModules.Core.Commands.Users.ChangeAvatar;
using UserModules.Core.Services;

namespace DigiLearn.Web.Pages.Profile
{
    public class IndexModel : BaseRazor
    {
        private readonly IUserFacade _userFacade;
        private readonly INotificationFacade _notificationFacade;

        public IndexModel(IUserFacade userFacade, INotificationFacade notificationFacade)
        {
            _userFacade = userFacade;
            _notificationFacade = notificationFacade;
        }
        public UserDto? UserDto { get; set; }
        public List<NotificationFilterData> NewNotification { get; set; }
        public async Task OnGet()
        {
            UserDto = await _userFacade.GetUserByPhoneNumber(User.GetPhoneNumber());
            var notification = await _notificationFacade.GetByFilter(new NotificationFilterParams()
            {
                IsSeen = false,
                PageId = 1,
                Take = 5,
                UserId = UserDto!.Id
            });
            NewNotification = notification.Data;
        }
        public async Task<IActionResult> OnPost(IFormFile avatar)
        {
            var result = await _userFacade.ChangeAvatar(new ChangeUserAvatarCommand
            {
                AvatarFile = avatar,
                UserId = User.GetUserId()
            });
            return RedirectAndShowAlert(result, Redirect("/profile"));
        }
    }
}
