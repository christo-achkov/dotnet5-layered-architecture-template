using Microsoft.Extensions.DependencyInjection;
using AAR.Service.Query;
using AAR.Service.Command;
using ARR.Service.Decoratee;
using System;
using System.Linq;
using System.Reflection;
using AAR.Repository.Example;
using AAR.Data.Context.AAR;
using Serilog;
using AAR.Service.Decoratee;
using Microsoft.Extensions.Configuration;

namespace AAR.Infrastructure.Extension
{
    public static class ServiceExtension
    {
        public static void AddMisc(IServiceCollection services, string applicationAssemblyName)
        {
            var applicationAssembly = Assembly.Load(applicationAssemblyName);
            services.AddControllers().AddApplicationPart(applicationAssembly);

            services.AddEntityFrameworkSqlite()
            .AddDbContext<AARContext>();

            services.AddSwaggerGen();
        }

        public static void AddSerilog(IConfiguration serilogConfig)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Seq(serilogConfig["SeqUrl"])
                .CreateLogger();

            Log.Information("Hello, {Name}!", Environment.UserName);
        }
        public static void AddCommandServices(IServiceCollection services, string namingConvention)
        {
            RegisterDependencyWithReflection(typeof(ICommandService<>), namingConvention, services);
        }

        public static void AddQueryServices(IServiceCollection services, string namingConvention)
        {
            RegisterDependencyWithReflection(typeof(IQueryService<,>), namingConvention, services);
        }

        private static void RegisterDependencyWithReflection(Type dependencyType, string namingConvention, IServiceCollection services)
        {
            var assembly = dependencyType.Assembly;

            var types = from type in assembly.GetTypes()
                        where !type.IsAbstract
                        where type.Name.EndsWith(namingConvention)
                        select type;

            foreach (Type type in types)
            {
                services.AddTransient(type.GetInterfaces().Single(), type);
            }
        }

        public static void AddCommandDecorators(IServiceCollection services)
        {
            Assembly assemblyCommand = typeof(ICommandService<>).Assembly;

            var mappingsCommand = from type in assemblyCommand.GetTypes()
                                  where !type.IsAbstract
                                  where !type.IsGenericType
                                  from i in type.GetInterfaces()
                                  where i.IsGenericType
                                  where i.GetGenericTypeDefinition() == typeof(ICommandService<>)
                                  select new { service = i, implementation = type };

            foreach (var mapping in mappingsCommand)
            {
                Type[] commandType = mapping.service.GetGenericArguments();
                Type transactionDecoratorType = typeof(TransactionCommandDecorator<>).MakeGenericType(commandType);
                Type loggingDecoratorType = typeof(LoggingCommandDecorator<>).MakeGenericType(commandType);

                services.AddTransient(mapping.service,
                    c => ActivatorUtilities.CreateInstance
                        (c, loggingDecoratorType, ActivatorUtilities.CreateInstance
                            (c, transactionDecoratorType, ActivatorUtilities.CreateInstance
                                (c, mapping.implementation))));
            }
        }

        public static void AddQueryDecorators(IServiceCollection services)
        {
            Assembly assemblyQuery = typeof(IQueryService<,>).Assembly;

            var mappingsQuery = from type in assemblyQuery.GetTypes()
                                where !type.IsAbstract
                                where !type.IsGenericType
                                from i in type.GetInterfaces()
                                where i.IsGenericType
                                where i.GetGenericTypeDefinition() == typeof(IQueryService<,>)
                                select new { service = i, implementation = type };

            foreach (var mapping in mappingsQuery)
            {
                Type[] queryType = mapping.service.GetGenericArguments();
                Type loggingDecoratorType = typeof(LoggingQueryDecorator<,>).MakeGenericType(queryType);

                services.AddTransient(mapping.service,
                    c => ActivatorUtilities.CreateInstance
                            (c, loggingDecoratorType, ActivatorUtilities.CreateInstance
                                (c, mapping.implementation)));
            }
        }

        public static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<IExampleRepository, ExampleRepository>();
        }
    }
}
