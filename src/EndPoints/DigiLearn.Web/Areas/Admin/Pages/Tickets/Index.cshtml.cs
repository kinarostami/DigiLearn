using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TIcketModules.Core.DTOs.Tickets;
using TIcketModules.Core.Services;

namespace DigiLearn.Web.Areas.Admin.Pages.Tickets;

public class IndexModel : BaseRazorFilter<TicketFilterParams>
{
    private readonly ITicketService _ticketService;

    public IndexModel(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }
    public TicketFilterReulst FilterReulst { get; set; }
    public async Task OnGet()
    {
        FilterReulst = await _ticketService.GetTicketsByFilter(FilterParams);
    }
}
