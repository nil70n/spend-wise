using System.Reflection;
using Ardalis.SharedKernel;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using SpendWise.Core.AccountAggregate;
using Module = Autofac.Module;

namespace SpendWise.Infrastructure;

/// <summary>
/// An Autofac module responsible for wiring up services defined in Infrastructure.
/// Mainly responsible for setting up EF and MediatR, as well as other one-off services.
/// </summary>
public class AutofacInfrastructureModule : Module
{
    private readonly bool _isDevelopment = false;
    private readonly List<Assembly> _assemblies = new List<Assembly>();

    public AutofacInfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
    {
        _isDevelopment = isDevelopment;
        AddToAssembliesIfNotNull(callingAssembly);
    }

    private void AddToAssembliesIfNotNull(Assembly? assembly)
    {
        if (assembly != null)
        {
            _assemblies.Add(assembly);
        }
    }

    private void LoadAssemblies()
    {
        // TODO: Replace these types with any type in the appropriate assembly/project
        var coreAssembly = Assembly.GetAssembly(typeof(Account));
        var infrastructureAssembly = Assembly.GetAssembly(typeof(AutofacInfrastructureModule));
        /* var useCasesAssembly = Assembly.GetAssembly(typeof()); */

        AddToAssembliesIfNotNull(coreAssembly);
        AddToAssembliesIfNotNull(infrastructureAssembly);
        /* AddToAssembliesIfNotNull(useCasesAssembly); */
    }

    protected override void Load(ContainerBuilder builder)
    {
        LoadAssemblies();

        if (_isDevelopment)
        {
            RegisterDevelopmentOnlyDependencies(builder);
        }
        else
        {
            RegisterProductionOnlyDependencies(builder);
        }
        RegisterEF(builder);
        RegisterQueries(builder);
        RegisterMediatR(builder);
    }

    private void RegisterEF(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(IRepository<>))
          .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(IReadRepository<>))
          .InstancePerLifetimeScope();
    }

    private void RegisterQueries(ContainerBuilder builder)
    {
    }

    private void RegisterMediatR(ContainerBuilder builder)
    {
        builder
          .RegisterType<Mediator>()
          .As<IMediator>()
          .InstancePerLifetimeScope();

        builder
          .RegisterGeneric(typeof(LoggingBehavior<,>))
          .As(typeof(IPipelineBehavior<,>))
          .InstancePerLifetimeScope();

        builder
          .RegisterType<MediatRDomainEventDispatcher>()
          .As<IDomainEventDispatcher>()
          .InstancePerLifetimeScope();

        var mediatrOpenTypes = new[]
        {
      typeof(IRequestHandler<,>),
      typeof(IRequestExceptionHandler<,,>),
      typeof(IRequestExceptionAction<,>),
      typeof(INotificationHandler<>),
    };

        foreach (var mediatrOpenType in mediatrOpenTypes)
        {
            builder
              .RegisterAssemblyTypes(_assemblies.ToArray())
              .AsClosedTypesOf(mediatrOpenType)
              .AsImplementedInterfaces();
        }
    }

    private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
    {
        // NOTE: Add any development only services here
    }

    private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
    {
        // NOTE: Add any production only (real) services here
    }
}
