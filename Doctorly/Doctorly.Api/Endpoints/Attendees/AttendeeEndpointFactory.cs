namespace Doctorly.Api.Endpoints.Attendees;

public class AttendeeEndpointFactory
{
    public Endpoint AddEndPoint => new Endpoint("/Attendee/AddAttendee", (context) =>
    {
        return Task.FromResult(new object());
    }, "Add Attendee", EndPointRestType.Post);
}