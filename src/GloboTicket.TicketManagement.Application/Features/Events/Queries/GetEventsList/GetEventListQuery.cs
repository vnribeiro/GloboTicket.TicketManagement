using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList;

public class GetEventListQuery : IRequest<List<EventListVm>> {}