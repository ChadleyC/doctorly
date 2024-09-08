namespace Doctorly.Api.Endpoints.Events;

public class EventEndpointFactory
{
    public List<Endpoint> EventEndPoints = new List<Endpoint>
    {
        new Endpoint("/Events/Add", () =>
        {
            
        }, "AddEvent", EndPointRestType.Post),
        
    };
}