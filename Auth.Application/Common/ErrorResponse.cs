namespace Auth.Application.Common;

public class ErrorResponse
{
    public string HttpStatus { get; set; } = "error";
    public int HttpStatusCode { get; set; } = 500;
    public string HttpRoute { get; set; }
    public string HttpMessage { get; set; }
    public string Type { get; set; }
    public string TraceId { get; set; }
    public string? StackTrace { get; set; }
}