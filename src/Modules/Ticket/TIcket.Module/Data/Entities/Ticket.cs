using Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIcketModules.Data.Entities;

class Ticket : BaseEntity
{
    public Guid UserId { get; set; }
    [MaxLength(100)]
    public string OwnerFullName { get; set; }

    [MaxLength(11)]
    public string PhoneNumber { get; set; }

    [MaxLength(80)]
    public string Title { get; set; }
    public string Text { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public List<TicketMessage> Messages { get; set; }
}

public enum TicketStatus
{
    Pending,
    Answered,
    Closed
}
