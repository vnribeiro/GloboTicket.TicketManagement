using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using GloboTicket.TicketManagement.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.TicketManagement.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(GloboTicketDbContext dbContext) 
        : base(dbContext) {}

    public async Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents)
    {
        var allCategories = await GloboTicketDbContext
            .Categories
            .Include(x => x.Events)
            .ToListAsync();

        if (!includePassedEvents)
        {
            allCategories.ForEach(p => p.Events!
                .ToList()
                .RemoveAll(c => c.Date < DateTime.Today));
        }

        return allCategories;
    }
}
