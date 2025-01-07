namespace UserIdentityService.Application.Common;

public class Response
{
    public string? Status { get; set; }
    public string? Message { get; set; }
    public bool? IsSuccess { get; set; }
}