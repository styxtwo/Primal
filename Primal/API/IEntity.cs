using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.API {
    public interface IEntity {
        event Action<IEntity> ComponentsChanged;

        bool Add(IComponent component);
        bool Remove<T>();
        T Get<T>();

        bool Contains<T>();
        bool Contains(Type type);
        bool ContainsAll(IEnumerable<Type> types);

        int ComponentCount { get; }

    }
}
