using System.Linq.Expressions;
using Doctorly.Data.Models;
using Doctorly.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Doctorly.Data.UseCases.Events;

public class EventService : IEventService
{
    private readonly DoctorlyDbContext _db;

    public EventService(DoctorlyDbContext db)
    {
        _db = db;
    }

    public async Task<Event?> GetEventAsync(Guid id)
    {
        return await _db.Events.FindAsync(id);
    }

    public async Task<Event?> AddEventAsync(Event item)
    {
        var result = await _db.Events.AddAsync(item);
        await _db.SaveChangesAsync();
        return result.State != EntityState.Added ? null : result.Entity;
    }

    public async Task<Event?> UpdateEventAsync(Event item)
    {
        var existingEvent = await GetEventAsync(item.Id);

        if (existingEvent == null)
        {
            throw new ArgumentException("Event does not exist");
        }

        var result = _db.Events.Update(item);

        if (result.State == EntityState.Modified)
        {
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        return null;
    }

    public async Task<bool> DeleteEventAsync(Guid id)
    {
        var existingEvent = await GetEventAsync(id);

        if (existingEvent == null)
        {
            throw new ArgumentException("Event does not exist");
        }

        var result = _db.Events.Remove(existingEvent);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Deleted;
    }

    public async Task<List<Event>?> GetAllActiveEventsAsync()
    {
        return await _db.Events
            .Where(x => x.EndTime <= DateTimeOffset.Now && x.Attendees.Exists(c => c.Accepted))
            .ToListAsync();
    }

    public async Task<List<Event>?> SearchEventsAsync(Expression<Func<Event, bool>> predicate)
    {
        return await _db.Events.Where(predicate).ToListAsync();
    }
}