using Primal.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal {
    public interface ISystem {
        IEnumerable<Type> KeyComponents { get; }

        void UpdateEntity(IEntity entity);

        void BeforeUpdate();

        void AfterUpdate();

        void EntityAdded(IEntity entity);

        void EntityRemoved(IEntity entity);
    }
}
