﻿using Solid.Practices.IoC;

namespace Solid.Bootstrapping
{
    /// <summary>
    /// Represents an application that has ioc container registrator.
    /// </summary>
    public interface IHaveContainerRegistrator
    {
        /// <summary>
        /// Gets the registrator.
        /// </summary>
        /// <value>
        /// The registrator.
        /// </value>
        IIocContainerRegistrator Registrator { get; }
    }
}