using AutoMapper;
using GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using GloboTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetail;
using GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList;
using GloboTicket.TicketManagement.Domain.Entities;

namespace GloboTicket.TicketManagement.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Events
        CreateMap<Event, EventListVm>().ReverseMap();
        CreateMap<Event, EventDetailVm>().ReverseMap();

        // Categories
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryListVm>();
        CreateMap<Category, CategoryEventListVm>();
    }
}
