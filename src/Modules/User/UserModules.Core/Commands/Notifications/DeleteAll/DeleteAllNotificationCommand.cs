using Common.Application;

namespace UserModules.Core.Commands.Notifications.DeleteAll;

public record DeleteAllNotificationCommand(Guid UserId) : IBaseCommand;
