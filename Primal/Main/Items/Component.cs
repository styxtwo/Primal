using Primal.API;

namespace Primal {
    /// <summary>
    /// Abstract Implementation of the IComponent Interface.
    /// </summary>
    public class Component : IComponent {
        public virtual void Dispose() {
            //Empty, to be overridden.
        }
    }
}
