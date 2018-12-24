using Autofac;
using FluentValidation;
using JsonDiffer.Application.Behavior;
using JsonDiffer.Application.Command;
using JsonDiffer.Application.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JsonDiffer.Infrastructure
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IAsyncRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(PushLeftJsonCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<>));
            builder.RegisterAssemblyTypes(typeof(PushRightJsonCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<>));
            builder.RegisterAssemblyTypes(typeof(DiffCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(PushRightJsonCommandValidation).GetTypeInfo().Assembly)
                   .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                   .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(PushLeftJsonCommandValidation).GetTypeInfo().Assembly)
                   .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                   .AsImplementedInterfaces();

            builder.Register<SingleInstanceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            builder.Register<MultiInstanceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                return t =>
                {
                    var resolved = (IEnumerable<object>)componentContext.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                    return resolved;
                };
            });
            
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
