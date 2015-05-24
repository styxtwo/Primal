using Primal.API;
using System;
using System.Collections.Generic;

namespace Primal {
    /// <summary>
    /// Abstract implementation of the ISystem interface.
    /// </summary>
    public abstract class AbstractSystem : ISystem {
        private ISet<Type> keyComponents;

        public AbstractSystem() {
            keyComponents = new HashSet<Type>();
            AddKeyComponents();
        }

        public abstract void AddKeyComponents();

        public void AddKeyComponent<T>() where T : IComponent {
            keyComponents.Add(typeof(T));
        }

        public virtual void UpdateEntity(IEntity entity, double elapsedMs) {
            //Empty, to be overridden.
        }

        public virtual void BeforeUpdate(double elapsedMs) {
            //Empty, to be overridden.
        }

        public virtual void AfterUpdate(double elapsedMs) {
            //Empty, to be overridden.
        }

        public virtual void EntityAdded(IEntity entity) {
            //Empty, to be overridden.
        }

        public virtual void EntityRemoved(IEntity entity) {
            //Empty, to be overridden.
        }

        public IEnumerable<Type> KeyComponents {
            get {
                return keyComponents;
            }
        }
    }
}
