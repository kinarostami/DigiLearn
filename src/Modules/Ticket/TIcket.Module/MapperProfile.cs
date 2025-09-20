using AutoMapper;
using TIcketModules.Core.DTOs.Tickets;
using TIcketModules.Data.Entities;

namespace TIcketModules;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Ticket, CreateTicketCommand>().ReverseMap();
        CreateMap<Ticket, TicketDto>().ReverseMap();
        CreateMap<TicketMessageDto, TicketMessage>().ReverseMap();
    }
}
