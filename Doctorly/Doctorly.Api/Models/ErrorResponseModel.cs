using System.Net;

namespace Doctorly.Api.Models;

public class ErrorResponseModel : ResponseModel
{
    public ErrorResponseModel(HttpStatusCode status, string errorMessage, string suggestion) : base(status)
    {
        ErrorMessage = errorMessage;
        Suggestion = suggestion;
    }
    public string ErrorMessage { get; }
    public string Suggestion { get; }
}