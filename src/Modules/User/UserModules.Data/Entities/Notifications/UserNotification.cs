using Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModules.Data.Entities.Notifications;

public class UserNotification : BaseEntity
{
    public Guid UserId { get; set; }
    [MaxLength(2000)]
    public string Text { get; set; }
    [MaxLength(300)]
    public string Title { get; set; }
    public bool IsSeen { get; set; }
}
