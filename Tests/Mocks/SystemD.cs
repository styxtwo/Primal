using Primal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.Tests {
    /// <summary>
    /// Provides a system which is a draw system, and thus only updated when the draw method is called on world.
    /// </summary>
    class SystemD : DrawSystem {
        public Boolean IsUpdated { get; set; }

        protected internal override void AddKeyComponents() {
            AddKeyComponent<ComponentD>();
        }

        protected internal override void BeforeUpdate(double elapsedMs) {
            IsUpdated = true;
        }

        protected internal override void UpdateEntity(IEntity entity, double elapsedMs) {
        }
    }
}
