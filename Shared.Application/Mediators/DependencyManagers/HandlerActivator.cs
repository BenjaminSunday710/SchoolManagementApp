using System;
using System.Linq;
using System.Reflection;

namespace Shared.Application.Mediators.DependencyManagers
{
    internal class HandlerActivator
    {
        public static TCommandHandler Activate<TCommandHandler>(IServiceProvider serviceProvider)
        {
            var handlerType = typeof(TCommandHandler);
            ConstructorInfo[] handlerCtorInfos = handlerType.GetConstructors();
            var parameteredCtor = handlerCtorInfos.FirstOrDefault(c => c.GetParameters().Length > 0);

            if (parameteredCtor == null) return (TCommandHandler)Activator.CreateInstance(typeof(TCommandHandler));

            var parameters = GetParameters(parameteredCtor, serviceProvider);
            return (TCommandHandler)Activator.CreateInstance(typeof(TCommandHandler), parameters);
        }

        private static object[] GetParameters(ConstructorInfo parameteredCtor, IServiceProvider serviceProvider)
        {
            var paramInfos = parameteredCtor.GetParameters();
            object[] parameters = new object[paramInfos.Length];

            for (int i = 0; i < paramInfos.Length; i++)
            {
                var paramType = paramInfos[i].ParameterType;
                if (!paramType.IsInterface) throw new ArgumentException("invalid constructor parameter");

                var paramImplementation = serviceProvider.GetService(paramType);
                if (paramImplementation == null) throw new ArgumentException("required type is not registered");
                parameters[i] = paramImplementation;
            }
            return parameters;
        }
    }
}
