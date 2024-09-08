namespace Doctorly.Api.Endpoints;

public static class RegisterEndpoints
{
    public static void RegisterAllEndPoints(this WebApplication app)
    {
        List<Endpoint> eventsEndPoints = new List<Endpoint>();
        List<Endpoint> attendeeEndPoints = new List<Endpoint>();

        var listToProcess = new List<Endpoint>();
        listToProcess.AddRange(eventsEndPoints);
        listToProcess.AddRange(attendeeEndPoints);
        
        foreach (var endpoint in listToProcess)
        {
            app?.MapGet(endpoint.EndPointUrl, endpoint.EndpointLogic)
                .WithName(endpoint.Name)
                .WithOpenApi();
        }
    }
}