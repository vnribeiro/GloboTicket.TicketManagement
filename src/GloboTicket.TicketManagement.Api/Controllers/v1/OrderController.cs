using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.TicketManagement.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{apiVersion:apiVersion}/order")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
    }
}
