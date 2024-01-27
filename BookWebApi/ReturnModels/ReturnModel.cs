using System.Net;

namespace BookWebApi.ReturnModels;
public class ReturnModel<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }

    public string Message { get; set; }

    public HttpStatusCode StatusCode { get; set; }
}
