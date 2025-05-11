namespace Talabat.API.Errors
{
    public class ServerErrorResponse :ApiResponse
    {
        public string?Details {  get; set; }
        public ServerErrorResponse(int code,string? message=null, string? details=null) :base(code,message)
        {
            Details=details;
        }
    }
}
