using Microsoft.OpenApi.Models;
using Twilio.Rest.Api.V2010.Account.Usage.Record;

namespace Api.Extentions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentions(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Demo", Version = "v1" });
                var secuity = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition("Bearer", secuity);
                var sercirtyRequirement = new OpenApiSecurityRequirement
                {
                    { 
                        secuity,new string[]{ }
                    }
                };
                c.AddSecurityRequirement(sercirtyRequirement);
            });
            return services;
        }
    }
}
