using Doctorly.Data.UseCases.Events;

namespace Doctorly.Api.Endpoints.Events;

public class EventEndpointFactory
{
    private readonly IServiceProvider _services;

    public EventEndpointFactory(IServiceProvider services)
    {
        _services = services;
    }
    
}