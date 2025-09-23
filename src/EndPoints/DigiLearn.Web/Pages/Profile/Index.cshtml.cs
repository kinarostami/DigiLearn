using DigiLearn.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Queries._DTOs;
using UserModule.Core.Services;
using UserModules.Core.Services;

namespace DigiLearn.Web.Pages.Profile
{
    public class IndexModel : PageModel
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
    }
}
