using Doctorly.Api.Endpoints.Attendees;
using Doctorly.Api.Endpoints.Events;

namespace Doctorly.Api.Endpoints;

public static class RegisterEndpoints
{
    public static void RegisterAllEndPoints(this WebApplication app)
    {
        EventEndpointFactory.MapEndpoints(app);
        AttendeeEndpointFactory.RegisterAttendeeEndpoints(app);
    }
}