using System.Net;
namespace Domain;
public class Response<T>
{
    public int StatusCode { get; set; }
    public string Messege { get; set; }
    public T Data { get; set; }
    public Response(T data)
    {
        StatusCode = (int)HttpStatusCode.OK;
        Data = data;
    }
    public Response(HttpStatusCode code) => StatusCode =(int)code;
    public Response(HttpStatusCode code, string messege)
    {
        Messege=messege;
        StatusCode = (int)code;
    }
}
