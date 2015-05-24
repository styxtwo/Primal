using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primal.API {
    /// <summary>
    /// Provides a component for entities to add.
    /// </summary>
    public interface IComponent {

        /// <summary>
        /// disposes of the component.
        /// </summary>
        void Dispose();
    }
}
