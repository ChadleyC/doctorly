using System.Linq.Expressions;
using System.Net;
using Doctorly.Api.Extensions;
using Doctorly.Api.Models;
using Doctorly.Data.Models;
using Doctorly.Data.UseCases.Events;

namespace Doctorly.Api.Endpoints;

public static class RegisterEndpoints
{
    public static void RegisterAllEndPoints(this WebApplication app)
    {
        app.MapGet("Events/GetEvent", new Func<Guid, IEventService, Task<ResponseModel>>(
                async (Guid eventId, IEventService eventService) =>
                {
                    if (eventId == Guid.Empty)
                    {
                        return new ErrorResponseModel(HttpStatusCode.BadRequest, "Invalid event Id",
                            "Provide valid id");
                    }

                    return new SuccessResponseModel(HttpStatusCode.OK,
                        await eventService.GetEventAsync(eventId) ?? new Event());
                }))
            .WithName("GetEvent")
            .WithOpenApi();

        app.MapGet("Events/SearchEvents",
                new Func<DateTimeOffset?, DateTimeOffset?, string, IEventService, Task<ResponseModel>>(
                    async (DateTimeOffset? startDate, DateTimeOffset? endDate, string title,
                        IEventService eventService) =>
                    {
                        Expression<Func<Event, bool>> where = e => true;
                        if (startDate is not null)
                        {
                            where.AppendWhere("StateTime", startDate);
                        }

                        if (endDate is not null)
                        {
                            where.AppendWhere("EndTime", endDate);
                        }

                        if (!string.IsNullOrEmpty(title))
                        {
                            where.AppendWhere("Title", title);
                        }

                        return new SuccessResponseModel(HttpStatusCode.OK,
                            await eventService.SearchEventsAsync(where) ?? new List<Event>());
                    }))
            .WithName("GetEvent")
            .WithOpenApi();
        
    }
}