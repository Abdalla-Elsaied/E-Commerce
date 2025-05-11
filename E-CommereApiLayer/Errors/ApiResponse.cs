
namespace Talabat.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int StatusCode, string? Message=null) 
        {
            this.StatusCode = StatusCode;
            this.Message = Message??GetDefaultMessage(StatusCode);
        }

        private string? GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Error are the path to dark side",
                _ => null
            };
        }
    }
}
