using System.Net;

namespace Doctorly.Api.Models;

public class SuccessResponseModel : ResponseModel
{
    public SuccessResponseModel(HttpStatusCode status, object result) : base(status)
    {
        Result = result;
    }
    public Object Result { get; }
}