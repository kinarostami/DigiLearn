using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using UserModule.Core.Queries._DTOs;
using UserModules.Core.Commands.Notifications.Delete;
using UserModules.Core.Commands.Notifications.DeleteAll;
using UserModules.Core.Commands.Notifications.Seen;
using UserModules.Core.Services;

namespace DigiLearn.Web.Pages.Profile
{
    [BindProperties]
    public class NotificationsModel : BaseRazorFilter<NotificationFilterParams>
    {
        private readonly INotificationFacade _notificationFacade;

        public NotificationsModel(INotificationFacade notificationFacade)
        {
            _notificationFacade = notificationFacade;
        }

        public NotificationFilterResult FilterResult { get; set; }
        public async Task OnGet()
        {
            FilterResult = await _notificationFacade.GetByFilter(new NotificationFilterParams()
            {
                IsSeen = null,
                PageId = FilterParams.PageId,
                Take = 6 ,
                UserId = User.GetUserId()
            });
        }

        public async Task<IActionResult> OnPostSeenNotification(Guid notificationId)
        {
            var result = await _notificationFacade.SeenNotification(new SeenNotificationCommand(notificationId));
            return RedirectAndShowAlert(result,Page());
        }

        public async Task<IActionResult> OnPostDeleteAll()
        {
            return await AjaxTryCatch(() =>
            {
                return  _notificationFacade.DeleteAll(new DeleteAllNotificationCommand(User.GetUserId()));
            });
        }

        public async Task<IActionResult> OnPostDeleteNotification(Guid notificationId)
        {
            return await AjaxTryCatch(() =>
            {
                return  _notificationFacade.Delete(new DeleteNotificationCommand(notificationId,User.GetUserId()));
            });
        }

    }
}
