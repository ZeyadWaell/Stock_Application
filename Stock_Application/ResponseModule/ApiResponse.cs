namespace Api.ResponseModule
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,string message =null)
        {
            StatusCode = statusCode;
            Messsage = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        public int StatusCode { get; set; }

        public string Messsage { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "ERROR 404",
                401 => "Error 401",
                500 => "Not found"
            };
        }
    }
}
