using System;
using System.Collections.Generic;
using System.Linq;

using Ninject.Modules;
using Piranha.Manager.Repositories;

namespace Piranha.Manager
{
	/// <summary>
	/// This is the default dependency injection module for the manager interface.
	/// </summary>
	internal sealed class InjectionModule : NinjectModule
	{
		/// <summary>
		/// Loads the module configuration.
		/// </summary>
		public override void Load() {
			Bind<IUserRepository>().To<UserRepository>() ;
			Bind<IPermissionRepository>().To<PermissionRepository>() ;
		}
	}
}