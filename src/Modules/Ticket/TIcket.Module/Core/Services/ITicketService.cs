using AutoMapper;
using Azure;
using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIcketModules.Core.DTOs.Tickets;
using TIcketModules.Data;
using TIcketModules.Data.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TIcketModules.Core.Services;

public interface ITicketService
{
    Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command);
    Task<OperationResult> SendMessageInTicket(SendTicketMessageCommand command);
    Task<OperationResult> CloseTicket(Guid ticketId);

    Task<TicketDto> GetTicket(Guid ticketId);
    Task<TicketFilterReulst> GetTicketsByFilter(TicketFilterParams filterParams);
}
class TicketService : ITicketService
{
    private readonly TicketContext _ticketContext;
    private readonly IMapper _mapper;

    public TicketService(TicketContext ticketContext, IMapper mapper)
    {
        _ticketContext = ticketContext;
        _mapper = mapper;
    }

    public async Task<OperationResult> CloseTicket(Guid ticketId)
    {
        var ticket = await _ticketContext.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId);
        if (ticket == null)
            return OperationResult.NotFound();

        ticket.TicketStatus = TicketStatus.Closed;
        _ticketContext.Tickets.Update(ticket);
        await _ticketContext.SaveChangesAsync();
        return OperationResult.Success();

    }

    public async Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command)
    {
        var ticket = _mapper.Map<Ticket>(command);

        _ticketContext.Tickets.Add(ticket);
        await _ticketContext.SaveChangesAsync();
        return OperationResult<Guid>.Success(ticket.Id);
    }

    public async Task<TicketDto> GetTicket(Guid ticketId)
    {
        var ticket = await _ticketContext.Tickets
            .Include(x => x.Messages)
            .FirstOrDefaultAsync(x => x.Id == ticketId);
        
       return _mapper.Map<TicketDto>(ticket);
    }

    public async Task<TicketFilterReulst> GetTicketsByFilter(TicketFilterParams filterParams)
    {
        var result =  _ticketContext.Tickets.AsQueryable();

        if(filterParams != null)
            result = result.Where(x => x.UserId == filterParams.UserId);

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var data = new TicketFilterReulst()
        {
            Data = await result.Skip(skip).Take(filterParams.Take)
            .Select(x => new TicketFilterData()
            {
                Id = x.Id,
                UserId = x.UserId,
                CreationDate = x.CreationDate,
                TicketStatus = x.TicketStatus,
                Title = x.Title
            }).ToListAsync()
        };
        data.GeneratePaging(result,filterParams.Take,filterParams.Take);
        return data;
    }

    public async Task<OperationResult> SendMessageInTicket(SendTicketMessageCommand command)
    {
        var ticket = await _ticketContext.Tickets.FirstOrDefaultAsync(x => x.Id == command.TicketId);
        if (ticket == null)
            return OperationResult.NotFound();

        var ticketMessage = new TicketMessage()
        {
            Text = command.Text.SanitizeText(),
            Ticket = ticket,
            TicketId = command.TicketId,
            UserId = command.UserId,
            OwnerFullName = command.OwnerFullName,
        };

        if(ticket.UserId == command.UserId)
        {
            ticket.TicketStatus = TicketStatus.Pending;
        }
        else
        {
            ticket.TicketStatus = TicketStatus.Answered;
        }

        _ticketContext.TicketMessages.Add(ticketMessage);
        _ticketContext.Tickets.Update(ticket);
        await _ticketContext.SaveChangesAsync();
        return OperationResult.Success();
    }
}
