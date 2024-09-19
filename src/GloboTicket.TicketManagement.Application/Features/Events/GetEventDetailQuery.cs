using GloboTicket.TicketManagement.Application.Features.Events.ViewModels;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events;

public class GetEventDetailQuery : IRequest<EventDetailVm>
{
    public Guid Id;
}
