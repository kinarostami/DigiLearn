using TIcketModules.Data.Entities;

namespace TIcketModules.Core.DTOs.Tickets;

class TicketMessageDto
{
    public Guid UserId { get; set; }
    public Guid TicketId { get; set; }
    public string OwnerFullName { get; set; }
    public string Text { get; set; }
    public DateTime CreationDate { get; set; }
    public Ticket Ticket { get; set; }
}