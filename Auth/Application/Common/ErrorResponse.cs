namespace Auth.Application.Common;

public class ErrorResponse
{
    public string Status { get; set; } = "error";
    public int StatusCode { get; set; }
    public string Route { get; set; }
    public string Message { get; set; }
    public string TraceId { get; set; }
}