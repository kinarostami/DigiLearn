
using Common.Query.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIcketModules.Data.Entities;

namespace TIcketModules.Core.DTOs.Tickets;

public class TicketFilterData
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string OwnerFullName { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public DateTime CreationDate { get; set; }
}
public class TicketFilterReulst : BaseFilter<TicketFilterData>
{
}
public class TicketFilterParams : BaseFilterParam
{
    public Guid? UserId { get; set; }
    public string? Title { get; set; }
    public TicketStatus? Status { get; set; }
}
    