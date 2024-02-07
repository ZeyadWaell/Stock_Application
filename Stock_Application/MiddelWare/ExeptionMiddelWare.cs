using Api.ResponseModule;
using System.Net;
using System.Text.Json;

namespace Api.MiddelWare
{
    public class ExeptionMiddelWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionMiddelWare> _logger;
        private readonly IHostEnvironment _environment;

        public ExeptionMiddelWare(RequestDelegate next, ILogger<ExeptionMiddelWare> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next(httpcontext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                httpcontext.Response.ContentType = "application/json";
                httpcontext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var respons = _environment.IsDevelopment()
                    ? new ApiExeption((int)HttpStatusCode.InternalServerError, ex.Message,ex.StackTrace.ToString())
                    : new ApiExeption((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(respons, options);

                await httpcontext.Response.WriteAsync(json);
            }
        }
    }
}
