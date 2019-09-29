using System.Data;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using SampleAPI.Service.Interface.Packaging;
using SampleAPI.ORM.Model.Packaging;
using SampleAPI.Service.Services.Packaging;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Converters;
using SampleAPI.Filters;
using SampleAPI.Middleware;
using SampleAPI.Service;
using SampleAPI.DataTransferObjects;
using SampleAPI.DataAcccess;
using SampleAPI.DataAcccess.Repositories.Packaging;
using SampleAPI.DataAcccess.RulesEngine;
using SampleAPI.ORM;
using SampleAPI.Interface.DataAccess;
using SampleAPI.Interface.DataAccess.Packaging;
using SampleAPI.Interface.Services;
using SampleAPI.DataTransferObjects.DTO.Packaging;





namespace SampleAPI.Initialization
{
    public class ServiceInitializer
    {
        public void RegisterServices(IMvcBuilder mvcBuilder, IServiceCollection services, IConfigurationRoot configuration)
        {
            //Inject AOP logger             
            services.AddScoped(lg => LogManager.GetLogger(typeof(LoggingMiddleware)));

            //Injecting DB Connection,Dapper,UnitOfWork
            //Injecting DB Connection,Dapper,UnitOfWork
            services.AddScoped<IDbConnection>(c => new System.Data.SqlClient.SqlConnection(configuration.GetSection("configuration:connectionStrings:1:connectionString").Value));
            services.AddScoped<IDapperWrapper, DapperWrapper>();
            services.AddScoped<IDbProvider, DbProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            //Injecting Business rules
            services.AddScoped(typeof(IRulesEngine<>), typeof(BaseRulesEngine<>));

            //Helper
            services.AddScoped<IDbHelper, DbHelper>();

            //Injecting all repositories
            services.AddScoped<ICustomerRepository<Customer>, CustomerRepository>();
            
            //Sevices
            services.AddScoped<ICustomerService, CustomerService>();
            


            //Helper

            //inject json settings
            services.Configure<ConfigurationSettings>(configuration.GetSection("ApplicationSettings"));

            mvcBuilder.AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter()); //Json serializer settings Enum as string
             });

            //Adding domain Events
       
          

            //Injecting API Versioning
            services.AddApiVersioning(c => {
                c.AssumeDefaultVersionWhenUnspecified = true;
                c.ApiVersionReader = new QueryStringOrHeaderApiVersionReader()
                {
                    HeaderNames = { "api-version" }
                };
            });

            //Injecting Filters
            services.AddScoped<ValidateActionFilterAttribute>();
            services.AddScoped<ExceptionActionFilterAttribute>();
            services.AddScoped<IExceptionService, ExceptionService>();

            mvcBuilder.AddMvcOptions(options =>
            {
                options.ModelValidatorProviders.Clear(); //Clearing existing validators since we are managing our own model validation.                                
            });

            services.Add(ServiceDescriptor.Scoped(typeof(IValidatorFactory), typeof(ServiceProviderValidatorFactory)));
            RegisterValidatorsFromAssemblyContainingType<BaseDto>(services);
            services.AddScoped<IValidationService, ValidationService>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Automapper to DI
            services.AddAutoMapper();
        }

        private void RegisterValidatorsFromAssemblyContainingType<T>(IServiceCollection services)
        {
            Assembly assembly = typeof(T).GetTypeInfo().Assembly;

            var openGenericType = typeof(IValidator<>);

            var query = from type in assembly.GetTypes().Where(c => !(c.GetTypeInfo().IsAbstract || c.GetTypeInfo().IsGenericTypeDefinition))
                let interfaces = type.GetInterfaces()
                let genericInterfaces = interfaces.Where(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == openGenericType)
                let matchingInterface = genericInterfaces.FirstOrDefault()
                where matchingInterface != null
                select new { matchingInterface, type };
            foreach (var pair in query)
            {
                services.Add(ServiceDescriptor.Transient(pair.matchingInterface, pair.type));
            }
        }
    }
}

