using AutoMapper;
using FluentValidation;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Models.Mail;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IValidator<CreateEventCommand> _validator;
    private readonly IEventRepository _eventRepository;
    private readonly IEmailService _emailService;

    public CreateEventCommandHandler(IMapper mapper,
        IValidator<CreateEventCommand> validator,
        IEventRepository eventRepository,
        IEmailService emailService)
    {
        _mapper = mapper;
        _validator = validator;
        _eventRepository = eventRepository;
        _emailService = emailService;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = _mapper.Map<Event>(request);

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new Exceptions.ValidationException(validationResult);
        }

        // Create a new event
        eventEntity = await _eventRepository.AddAsync(eventEntity);

        await SendEmailNotification(request);

        return eventEntity.EventId;
    }

    private async Task SendEmailNotification(CreateEventCommand request)
    {
        var email = new Email()
        {
            To = "teste@teste.com",
            Body = $"A new event was created: {request}",
            Subject = "A new event was created"
        };

        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
