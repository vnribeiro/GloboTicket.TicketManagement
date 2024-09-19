using GloboTicket.TicketManagement.Application.Features.Events.ViewModels;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events;

public class GetEventListQuery : IRequest<List<EventListVm>> {}