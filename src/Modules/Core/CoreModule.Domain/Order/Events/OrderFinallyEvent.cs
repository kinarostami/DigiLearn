using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Order.Events;

public class OrderFinallyEvent : BaseDomainEvent
{
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
}
