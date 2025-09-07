using System.Net;

namespace Common.c.ApiResponse;
public class ApiResponse
{
    public string CopyRight { get; set; } = "CopyRight 'Roydev Auto'. All rights are reserved";
    public string CreatedDate { get; set; } = getDate();
    public ICollection<string>? Errors { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    /// <returns>Current UTC Time in format (YYYY-MM-DDTHH:MM:SSZ)</returns>
    private static string getDate()
    {
        var r = DateTime.UtcNow.ToString("s");
        if (r != null)
        {
            return r + 'Z';
        }
        return "unknown";
    }
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

}

public static class ApiResponseExtensions
{
    public static string ToJSON(this ApiResponse response)
      => System.Text.Json.JsonSerializer.Serialize(response);

    public static ApiResponse BadAction(string message)
        => BadAction(message, HttpStatusCode.BadRequest);

    public static ApiResponse BadAction(string messаge, HttpStatusCode statausId)
       => new() { Errors = [messаge], StatusCode = statausId };

    public static ApiResponse BadAction(ICollection<string> messаges, HttpStatusCode statausId)
       => new() { Errors = [.. messаges], StatusCode = statausId };

    public static ApiResponse Success()
      => new() { StatusCode = HttpStatusCode.OK };

    public static ApiResponse<T> SuccessWithData<T>(T data) where T : class
      => new()
      {
          Data = data,
          StatusCode = HttpStatusCode.OK
      };
}