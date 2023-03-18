using System;
using System.Linq;
using System.Reflection;

namespace Shared.Application.Mediators.DependencyManagers
{
    internal static class PropertyDependencyExtension
    {
        public static void ManagePropertyInjection<TCommandHandler>(this TCommandHandler handler,IServiceProvider serviceProvider)
        {
            var handlerType = handler.GetType();
            PropertyInfo[] propertyInfos = handlerType.GetProperties();
            var properties = propertyInfos.Where(p => p.PropertyType.IsInterface);

            foreach ( var propertyInfo in properties) 
            {
                var service = serviceProvider.GetService(propertyInfo.PropertyType);
                if (service != null) propertyInfo.SetValue(handler, service);
            }
        }

    }
}
