using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Features.Events.ViewModels;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events;

public class GetEventsListQueryHandler : IRequestHandler<GetEventListQuery, List<EventListVm>>
{
    private readonly IAsyncRepository<Event> _eventRepository;
    private readonly IMapper _mapper;

    public GetEventsListQueryHandler(IAsyncRepository<Event> eventRepository, 
        IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<List<EventListVm>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
    {
        var events = (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);
        return _mapper.Map<List<EventListVm>>(events);
    }
}
