using System.Net;

namespace Doctorly.Api.Models;

public class ResponseModel
{
    public ResponseModel(HttpStatusCode status)
    {
        Status = status;
    }
    public HttpStatusCode Status { get; }
}