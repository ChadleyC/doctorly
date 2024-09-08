using System.Linq.Expressions;
using System.Net;
using Doctorly.Api.Extensions;
using Doctorly.Api.Models;
using Doctorly.Data.Models;
using Doctorly.Data.UseCases.Events;
using Microsoft.AspNetCore.Mvc;

namespace Doctorly.Api.Endpoints.Events;

public static class EventEndpointFactory
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapGet("Events/GetEvent", new Func<Guid, IEventService, Task<ResponseModel>>(
                async ([FromQuery] Guid eventId, [FromServices] IEventService eventService) =>
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
                    async ([FromQuery] DateTimeOffset? startDate, [FromQuery] DateTimeOffset? endDate, [FromQuery] string title,
                        [FromServices] IEventService eventService) =>
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

                        try
                        {
                            return new SuccessResponseModel(HttpStatusCode.OK,
                                await eventService.SearchEventsAsync(where) ?? new List<Event>());
                        }
                        catch (Exception
                               e) // added this to catch error and communicate but probably can be done with middleware more elegantly
                        {
                            return new ErrorResponseModel(HttpStatusCode.BadRequest, e.Message, string.Empty);
                        }
                    }))
            .WithName("SearchEvents")
            .WithOpenApi();

        app.MapPost("Events/AddEvent", new Func<Event, IEventService, Task<ResponseModel>>(
                async ([FromBody] Event item, [FromServices] IEventService eventService) =>
                {
                    try
                    {
                        var result = await eventService.AddEventAsync(item);

                        if (item == null)
                        {
                            return new ErrorResponseModel(HttpStatusCode.BadRequest,
                                "Error occured when saving the event, please try again", string.Empty);
                        }
                        
                        return new SuccessResponseModel(HttpStatusCode.OK,
                            result ?? new Event());
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponseModel(HttpStatusCode.BadRequest,
                            e.Message, "Error occured when saving the event, please try again");
                    }
                }))
            .WithName("AddEvent")
            .WithOpenApi();
        
        app.MapPatch("Events/UpdateEvent", new Func<Event, IEventService, Task<ResponseModel>>(
                async ([FromBody] Event item, [FromServices] IEventService eventService) =>
                {
                    var result = await eventService.UpdateEventAsync(item)
                        .ConfigureAwait(false);

                    if (result is null)
                    {
                        return new ErrorResponseModel(HttpStatusCode.BadRequest,
                            "Error occured when saving the event, please try again", string.Empty);
                    }

                    return new SuccessResponseModel(HttpStatusCode.OK,
                        result ?? new Event());
                })).WithName("UpdateEvent")
            .WithOpenApi();

        app.MapDelete("Events/DeleteEvent", 
            new Func<Guid, IEventService, Task<ResponseModel>>(async ([FromQuery] Guid id, [FromServices] IEventService eventService) =>
        {
            try
            {
                var result = await eventService.DeleteEventAsync(id);

                if (result)
                {
                    return new SuccessResponseModel(HttpStatusCode.OK,
                        new { success = result });
                }
                
                return new ErrorResponseModel(HttpStatusCode.BadRequest,
                    "Error occured when deleting the event, please try again", string.Empty);
            }
            catch (Exception e)
            {
                return new ErrorResponseModel(HttpStatusCode.BadRequest,
                    "Error occured when deleting the event, please try again", string.Empty);
            }
        }));
    }
}