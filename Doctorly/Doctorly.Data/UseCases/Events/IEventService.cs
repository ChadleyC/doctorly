using System.Linq.Expressions;
using Doctorly.Data.Models;

namespace Doctorly.Data.UseCases.Events;

public interface IEventService
{
    Task<Event?> GetEventAsync(Guid id);
    Task<Event?> AddEventAsync(Event item);
    Task<Event?> UpdateEventAsync(Event item);
    Task<bool> DeleteEventAsync(Guid id);
    Task<List<Event>?> GetAllActiveEventsAsync();
    Task<List<Event>?> SearchEventsAsync(Expression<Func<Event, bool>> predicate);
}