namespace Doctorly.Api.Endpoints;

public class Endpoint
{
    public Endpoint(string endPointUrl, RequestDelegate endpointLogic, string name, EndPointRestType restType)
    {
        EndPointUrl = endPointUrl;
        EndpointLogic = endpointLogic;
        Name = name;
        RestType = restType;
    }
    
    public string EndPointUrl { get; }
    public RequestDelegate EndpointLogic { get; }
    public string Name { get; }
    public EndPointRestType RestType { get; }
}

public enum EndPointRestType
{
    Get = 0,
    Post = 1,
    Put = 2, 
    Delete = 3,
    Patch = 4
}