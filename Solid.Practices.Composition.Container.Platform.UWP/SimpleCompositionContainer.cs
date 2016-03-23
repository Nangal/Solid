﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Practices.Modularity;

namespace Solid.Practices.Composition.Container
{
    /// <summary>
    /// Represents a basic implementation of composition container with export feature only.
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <seealso cref="Composition.ICompositionContainer{TModule}" />
    public class SimpleCompositionContainer<TModule> : ICompositionContainer<TModule>   
        where TModule : ICompositionModule     
    {
        private readonly IEnumerable<Assembly> _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCompositionContainer{TModule}"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>        
        public SimpleCompositionContainer(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        /// <summary>
        /// Collection of composition modules.
        /// </summary>            
        private List<TModule> Modules { get; set; }

        /// <summary>
        /// Collection of composition modules.
        /// </summary>            
        IEnumerable<TModule> ICompositionModulesProvider<TModule>.Modules
        {
            get { return Modules; }
        }

        void ICompositionContainer<TModule>.Compose()
        {
            Modules = new List<TModule>();
            foreach (var assembly in _assemblies)
            {
                InspectAssembly(assembly);
            }            
        }

        private void InspectAssembly(Assembly assembly)
        {
            var types = assembly.DefinedTypes;
            foreach (var typeInfo in types)
            {                
                if (typeInfo.IsClass && typeInfo.IsAbstract == false
                    && typeInfo.ImplementedInterfaces.Contains(typeof(ICompositionModule)))
                {                    
                    Modules.Add((TModule)Activator.CreateInstance(typeInfo.AsType()));
                }
            }
        }
    }
}
