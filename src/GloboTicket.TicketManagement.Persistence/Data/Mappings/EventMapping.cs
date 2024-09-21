using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GloboTicket.TicketManagement.Persistence.Data.Mappings;

public class EventMapping : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder
            .ToTable("Events");

        builder
            .HasKey(e => e.EventId);

        builder
            .Property(e => e.EventId)
            .IsRequired();

        builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
