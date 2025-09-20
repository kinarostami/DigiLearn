using TIcketModules.Data.Entities;

namespace TIcketModules.Core.DTOs.Tickets;

public  class TicketDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string OwnerFullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime CreationDate { get; set; }
    public List<TicketMessage> Messages { get; set; }
}
