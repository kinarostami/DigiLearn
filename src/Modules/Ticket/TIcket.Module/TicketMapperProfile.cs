using AutoMapper;
using TIcketModules.Core.DTOs.Tickets;
using TIcketModules.Data.Entities;

namespace TIcketModules;

public class TicketMapperProfile : Profile
{
    public TicketMapperProfile()
    {
        CreateMap<Ticket, CreateTicketCommand>().ReverseMap();
        CreateMap<Ticket, TicketDto>().ReverseMap();
        CreateMap<TicketMessageDto, TicketMessage>().ReverseMap();
    }
}
