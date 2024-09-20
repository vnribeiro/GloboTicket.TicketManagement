using AutoMapper;
using FluentValidation;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;
    private readonly IValidator<CreateEventCommand> _validator;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IMapper mapper,
        IValidator<CreateEventCommand> validator,
        IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = _mapper.Map<Event>(request);

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new Exceptions.ValidationException(validationResult);
        }

        return (await _eventRepository.AddAsync(eventEntity)).EventId;
    }
}
