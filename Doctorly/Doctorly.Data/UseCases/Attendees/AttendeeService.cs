using Doctorly.Data.Models;
using Doctorly.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Doctorly.Data.UseCases.Attendees;

public class AttendeeService : IAttendeeService
{
    private readonly DoctorlyDbContext _db;

    public AttendeeService(DoctorlyDbContext db)
    {
        _db = db;
    }
    
    private static Attendee? GetAttendeeFromEvent(Guid attendeeId, Event? existingEvent)
    {
        var attendee = existingEvent.Attendees.Find(x => x.Id == attendeeId);

        if (attendee == null)
        {
            throw new ArgumentException("Attendee does not exist");
        }

        return attendee;
    }

    
    private async Task<Event?> GetExistingEvent(Guid eventId)
    {
        var existingEvent = await _db.Events.FindAsync(eventId);

        if (existingEvent == null)
        {
            throw new ArgumentException("Event does not exist");
        }

        return existingEvent;
    }
    
    private async Task<bool> AcceptReject(Guid eventId, Guid attendeeId, bool accept)
    {
        var existingEvent = await GetExistingEvent(eventId);
        var attendee = GetAttendeeFromEvent(attendeeId, existingEvent);

        attendee.Accepted = accept;
        existingEvent.Attendees[existingEvent.Attendees.IndexOf(attendee)] = attendee;
        return await UpdateEvent(existingEvent);
    }
    
    private async Task<bool> UpdateEvent(Event existingEvent)
    {
        var result = _db.Events.Update(existingEvent);
        await _db.SaveChangesAsync();
        return result.State == EntityState.Modified;
    }
    
    public async Task<bool> AddAttendeesToEvent(Guid eventId, Attendee[] attendees)
    {
        var existingEvent = await GetExistingEvent(eventId);

        existingEvent.Attendees?.AddRange(attendees);
        return await UpdateEvent(existingEvent);
    }

    public async Task<bool> RemoveAttendeeFromEvent(Guid eventId, Guid attendeeId)
    {
        var existingEvent = await GetExistingEvent(eventId);

        var attendee = GetAttendeeFromEvent(attendeeId, existingEvent);

        existingEvent.Attendees.Remove(attendee);
        return await UpdateEvent(existingEvent);
    }
    
    public async Task<List<Attendee>?> GetAttendeesFromEvent(Guid eventId)
    {
        var existingEvent = await GetExistingEvent(eventId);

        return existingEvent.Attendees;
    }

    public async Task<bool> AttendeeAccept(Guid eventId, Guid attendeeId)
    {
        return await AcceptReject(eventId, attendeeId, true);
    }

    public async Task<bool> AttendeeReject(Guid eventId, Guid attendeeId)
    {
        return await AcceptReject(eventId, attendeeId, false);
    }
}