using GloboTicket.TicketManagement.Application.Contracts;
using GloboTicket.TicketManagement.Domain.Entities;
using GloboTicket.TicketManagement.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace GloboTicket.TicketManagement.Persistence.IntegrationTests
{
    public class GloboTicketDbContextTests
    {
        private readonly GloboTicketDbContext _globoTicketDbContext;
        private readonly string _loggedInUserId;

        public GloboTicketDbContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<GloboTicketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _loggedInUserId = "00000000-0000-0000-0000-000000000000";
            var loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

            _globoTicketDbContext = new GloboTicketDbContext(dbContextOptions, loggedInUserServiceMock.Object);
        }

        [Fact]
        public async Task Save_SetCreatedByProperty()
        {
            var ev = new Event
            {
                EventId = Guid.NewGuid(), 
                Name = "Test event"
            };

            _globoTicketDbContext.Events.Add(ev);
            await _globoTicketDbContext.SaveChangesAsync();

            ev.CreatedBy.ShouldBe(_loggedInUserId);
        }
    }
}
