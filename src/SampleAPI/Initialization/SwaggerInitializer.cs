using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.Swagger.Model;

namespace SampleAPI.Initialization.Swagger
{
    public static class SwaggerInitializer
    {
        /// <summary>
        /// To register swagger implementation
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterSwagger(
            this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "SampleAPI - Microservices",
                    Description = "Microservices for SampleAPI Module",
                    TermsOfService = "None"
                });
            });

            return services;
        }
    }
}
