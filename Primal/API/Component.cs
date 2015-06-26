﻿using Primal.Api;

namespace Primal {

    /// <summary>
    /// Provides a component for entities to add. The basic data carrier of the ECS framework.
    /// </summary>
    public abstract class Component {

        /// <summary>
        /// disposes of the component.
        /// </summary>
        public virtual void Dispose() {
        }
    }
}
