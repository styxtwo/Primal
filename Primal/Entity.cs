using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;
namespace Primal {
    public class Entity : IEntity {
        public event Action<IEntity> ComponentsChanged;
        private IDictionary<Type, IComponent> components;

        public Entity() {
            components = new Dictionary<Type, IComponent>();
        }

        public bool Add(IComponent component) {
            if (components.ContainsKey(component.GetType())) {
                return false;
            }
            components.Add(component.GetType(), component);
            ComponentsChanged.NullSafeInvoke(this);
            return true;
        }

        public bool Remove<T>() {
            if (components.Remove(typeof(T))) {
                ComponentsChanged.NullSafeInvoke(this);
                return true;
            }
            return false;
        }

        public bool Contains<T>() {
            return Contains(typeof(T));
        }

        public bool Contains(Type type) {
            return components.ContainsKey(type);
        }

        public bool ContainsAll(IEnumerable<Type> types) {
            foreach (Type type in types) {
                if (!Contains(type)) {
                    return false;
                }
            }
            return true;
        }

        public T Get<T>(){
            IComponent component;
            components.TryGetValue(typeof(T), out component);
            return (T)component;
        }

        public int ComponentCount {
            get {
                return components.Count;
            }
        }
    }
}
