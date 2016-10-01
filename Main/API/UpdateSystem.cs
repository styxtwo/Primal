using System;

namespace Primal.Api
{
	/// <summary>
	/// Provides a system that contains the logic used for of the entities. 
	/// Seperated from the base systems to allow both the update and draw systems to be updated seperately.
	/// </summary>
	public abstract class UpdateSystem : AbstractSystem
	{
		protected internal override Boolean IsDraw()
		{
			return false;
		}
	}
}
