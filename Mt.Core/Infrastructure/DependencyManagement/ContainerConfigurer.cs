using System;
using System.Collections.Generic;
using System.Linq;
using Mt.Core.Configuration;

namespace Mt.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Configures the inversion of control container with services used by Mt.
    /// </summary>
    public class ContainerConfigurer
    {
        public virtual void Configure(IEngine engine, ContainerManager containerManager, MtConfig configuration)
        {
            //other dependencies
            containerManager.AddComponentInstance<MtConfig>(configuration, "Mt.configuration");
            containerManager.AddComponentInstance<IEngine>(engine, "Mt.engine");
            containerManager.AddComponentInstance<ContainerConfigurer>(this, "Mt.containerConfigurer");

            //type finder
            containerManager.AddComponent<ITypeFinder, WebAppTypeFinder>("Mt.typeFinder");

            //register dependencies provided by other assemblies
            var typeFinder = containerManager.Resolve<ITypeFinder>();
            containerManager.UpdateContainer(x =>
            {
                var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                var drInstances = new List<IDependencyRegistrar>();
                foreach (var drType in drTypes)
                    drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
                //sort
                drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
                foreach (var dependencyRegistrar in drInstances)
                    dependencyRegistrar.Register(x, typeFinder);
            });
        }
    }
}
