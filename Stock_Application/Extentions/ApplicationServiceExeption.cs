using Api.Helpers;
using Api.ResponseModule;
using Core.Interface;
using Infrastraction.Data;
using Infrastraction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extentions
{
    public static class ApplicationServiceExeption
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IUniterofWork, UniteofWork>();

            services.AddScoped<IOrderRepostory,OrderRepostory>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockHolderRepostory, StockHolderRepostory>();
            services.AddAutoMapper(typeof(MappingProfile));
       
            services.Configure<ApiBehaviorOptions>(options =>
            options.InvalidModelStateResponseFactory = ActionContext =>
            {
                var errors = ActionContext.ModelState.Where(e => e.Value.Errors.Count > 0).SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage);

                var errorRespinse = new ApiValidationResponse
                {
                    Errors = errors,
                };

                return new BadRequestObjectResult(errorRespinse);
            }
            );
            return services;
        }
        
    }
}
