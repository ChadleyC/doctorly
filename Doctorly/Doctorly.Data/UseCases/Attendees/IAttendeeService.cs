using Doctorly.Data.Models;

namespace Doctorly.Data.UseCases.Attendees;

public interface IAttendeeService
{
    Task<bool> AddAttendeesToEvent(Guid eventId, Attendee[] attendees);
    Task<bool> RemoveAttendeeFromEvent(Guid eventId, Guid attendeeId);
    Task<List<Attendee>?> GetAttendeesFromEvent(Guid eventId);
    Task<bool> AttendeeAccept(Guid eventId, Guid attendeeId);
    Task<bool> AttendeeReject(Guid eventId, Guid attendeeId);
}