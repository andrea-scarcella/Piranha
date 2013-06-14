using System;
using System.Collections.Generic;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// Manager list model for permissions.
	/// </summary>
	public class PermissionListModel
	{
		/// <summary>
		/// Permission model.
		/// </summary>
		public class PermissionModel 
		{
			/// <summary>
			/// Gets/sets the permission id.
			/// </summary>
			public Guid Id { get ; set ; }

			/// <summary>
			/// Gets/sets the permission name.
			/// </summary>
			public string Name { get ; set ; }

			/// <summary>
			/// Get/sets the group name.
			/// </summary>
			public string GroupName { get ; set ; }

			/// <summary>
			/// Gets/sets if the groups is locked.
			/// </summary>
			public bool IsLocked { get ; set ; }

			/// <summary>
			/// Gets/sets the date the permission was created.
			/// </summary>
			public DateTime Created { get ; set ; }

			/// <summary>
			/// Gets/sets the date the permission was last updated.
			/// </summary>
			public DateTime Updated { get ; set ; }
		}

		/// <summary>
		/// Gets/sets the currently available permissions.
		/// </summary>
		public IList<PermissionModel> Permissions { get ; set ; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PermissionListModel() {
			Permissions = new List<PermissionModel>() ;
		}
	}
}