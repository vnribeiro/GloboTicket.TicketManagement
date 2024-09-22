using GloboTicket.TicketManagement.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;

namespace GloboTicket.TicketManagement.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly GloboTicketDbContext GloboTicketDbContext;

    public BaseRepository(GloboTicketDbContext dbContext)
    {
        GloboTicketDbContext = dbContext;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await GloboTicketDbContext
            .Set<T>()
            .FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await GloboTicketDbContext
            .Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await GloboTicketDbContext.Set<T>().AddAsync(entity);
        await GloboTicketDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        GloboTicketDbContext.Entry(entity).State = EntityState.Modified;
        await GloboTicketDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        GloboTicketDbContext.Set<T>().Remove(entity);
        await GloboTicketDbContext.SaveChangesAsync();
    }
}