using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    class SystemBC : BaseSystem {
        protected internal override void AddKeyComponents() {
            AddKeyComponent<ComponentB>();
            AddKeyComponent<ComponentC>();
        }

        protected internal override void UpdateEntity(IEntity entity, double elapsedMs) {
        }
    }
}
