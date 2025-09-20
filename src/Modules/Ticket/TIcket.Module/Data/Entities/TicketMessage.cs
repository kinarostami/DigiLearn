using Common.Domain;
using System.ComponentModel.DataAnnotations;

namespace TIcketModules.Data.Entities;

class TicketMessage : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TicketId { get; set; }

    [MaxLength(80)]
    public string OwnerFullName { get; set; }
    public string Text { get; set; }
    public Ticket Ticket { get; set; }
}
