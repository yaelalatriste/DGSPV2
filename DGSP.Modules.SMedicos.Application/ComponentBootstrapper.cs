using DGSP.Modules.SMedicos.Contract.Queries.Siacom;
using DGSP.Shared.Abstractions.Commands;
using DGSP.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DGSP.Modules.SMedicos.Application
{
    public static class ComponentBootstrapper
    {
        private static readonly Assembly[] ContractAssemblies = new[] { typeof(ObtenerConsultoriosSIACOMQuery).Assembly };
        private static readonly Assembly[] BusinessLayerAssemblies = new[] { Assembly.GetExecutingAssembly(), typeof(ObtenerConsultoriosSIACOMQuery).Assembly };

        public static void Bootstrap(IServiceCollection container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            //container.RegisterInstance<ILoggerFactory>(new LoggerFactory());
            //var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperModuloProfile>(), container.Resolve<ILoggerFactory>());

            //IMapper mapper = config.CreateMapper();
            //container.RegisterInstance<IMapper>(mapper);



            //***********************************************************************************
            //Se registra en tiempo de ejecución todos los objetos que heredan de ICommandHandler 
            //***********************************************************************************


            container.Scan(scaner =>
            scaner.FromAssemblies(BusinessLayerAssemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>))
                                  .Where(t => !t.IsAbstract && !t.ContainsGenericParameters))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>))
                                  .Where(t => !t.IsAbstract && !t.ContainsGenericParameters))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
            //**************************************************************************************************
            // se registra el decorador de transactionScope
            // *************************************************************************************************
            /*container.RegisterType(typeof(ICommandHandler<,>),
                                typeof(TransactionCommandHandlerDecorator<,>),
                                    new InjectionConstructor(new ResolvedParameter(typeof(ICommandHandler<,>), "Base"))
                                );*/
            //**************************************************************************************************

            //**************************************************************************************************
            // se registra el decorador de ExceptionCommandHandlerDecorator
            // *************************************************************************************************
            //container.RegisterType(typeof(ICommandHandler<,>),
            //                    typeof(ExceptionCommandHandlerDecorator<,>),
            //                        new InjectionConstructor(new ResolvedParameter(typeof(ICommandHandler<,>), "Base"))
            //                    );
            //**************************************************************************************************

            /*handlerRegistrations = from assembly in BusinessLayerAssemblies
                                   from implementation in assembly.GetExportedTypes()
                                   where !implementation.IsAbstract
                                   where !implementation.ContainsGenericParameters
                                   let services = (from iface in implementation.GetInterfaces()
                                                   where iface.IsGenericType
                                                   where iface.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)
                                                   select iface
                                                   )
                                   from service in services
                                   select new { service, implementation };*/

            /*foreach (var obj in handlerRegistrations)
            {
                container.RegisterType(obj.service, obj.implementation);
            }*/


            //container.RegisterInstance<IValidator>(new DataAnnotationsValidator());

            //container.Register(typeof(ICommandHandler<>), BusinessLayerAssemblies);
            //container.RegisterDecorator(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
            //container.RegisterInstance(typeof(ICommandHandler<,>), typeof(ExceptionCommandHandlerDecorator<,>));

            //container.Register(typeof(IQueryHandler<,>), BusinessLayerAssemblies);
            //container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(ValidationQueryHandlerDecorator<,>));
            //container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(AuthorizationQueryHandlerDecorator<,>));
        }

        //public static IEnumerable<Type> GetCommandTypes() =>
        //    from assembly in ContractAssemblies
        //    from type in assembly.GetExportedTypes()
        //    where typeof(ICommand).IsAssignableFrom(type)
        //    where !type.IsAbstract
        //    select type;
        public static IEnumerable<(Type CommandType, Type ResultType)> GetCommandTypes() =>
           from assembly in ContractAssemblies
           from type in assembly.GetExportedTypes()
           where IsCommand(type)
           where !type.IsAbstract
           select (type, DetermineCommandResultTypes(type).Single());

        //public static Type CreateQueryHandlerType(Type queryType) =>
        //    typeof(IQueryHandler<,>).MakeGenericType(queryType, DetermineQueryResultTypes(queryType).Single());

        public static IEnumerable<(Type QueryType, Type ResultType)> GetQueryTypes() =>
            from assembly in ContractAssemblies
            from type in assembly.GetExportedTypes()
            where IsQuery(type)
            select (type, DetermineQueryResultTypes(type).Single());

        public static Type GetQueryResultType(Type queryType) => DetermineQueryResultTypes(queryType).Single();
        public static Type GetCommandResultType(Type commandType) => DetermineCommandResultTypes(commandType).Single();

        private static bool IsQuery(Type type) => DetermineQueryResultTypes(type).Any();
        private static bool IsCommand(Type type) => DetermineCommandResultTypes(type).Any();


        private static IEnumerable<Type> DetermineQueryResultTypes(Type type) =>
            from interfaceType in type.GetInterfaces()
            where interfaceType.IsGenericType
            where interfaceType.GetGenericTypeDefinition() == typeof(IQuery<>)
            select interfaceType.GetGenericArguments()[0];
        private static IEnumerable<Type> DetermineCommandResultTypes(Type type)
        {
            var tmp = type.GetInterfaces();

            var rpta = from interfaceType in type.GetInterfaces()
                       where interfaceType.IsGenericType
                       where interfaceType.GetGenericTypeDefinition() == typeof(ICommand<>)
                       select interfaceType.GetGenericArguments()[0];

            return rpta;
        }
    }
}