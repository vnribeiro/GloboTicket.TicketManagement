using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetail;

public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailVm>
{
    private readonly IAsyncRepository<Event> _eventRepository;
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetEventDetailQueryHandler(IAsyncRepository<Event> eventRepository,
        IAsyncRepository<Category> categoryRepository,
        IMapper mapper)
    {
        _eventRepository = eventRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<EventDetailVm> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
    {
        var eventData = await _eventRepository.GetByIdAsync(request.Id);
        var eventDetailDto = _mapper.Map<EventDetailVm>(eventData);

        // Get the category and map to DTO
        var category = await _categoryRepository.GetByIdAsync(eventData.CategoryId);
        eventDetailDto.Category = _mapper.Map<CategoryDto>(category);
        return eventDetailDto;
    }
}
