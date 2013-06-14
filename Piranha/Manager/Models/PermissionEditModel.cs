using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// Manager edit model for permissions.
	/// </summary>
	public class PermissionEditModel
	{
		/// <summary>
		/// The groups available for a permission.
		/// </summary>
		public class PermissionGroup {
			/// <summary>
			/// Gets/sets the group id.
			/// </summary>
			public Guid Id { get ; set ; }

			/// <summary>
			/// Gets/sets the group name.
			/// </summary>
			public string Name { get ; set ; }
		}

		/// <summary>
		/// Gets/sets the permission id.
		/// </summary>
		public Guid Id { get ; set ; }
	
		/// <summary>
		/// Gets/sets the group id.
		/// </summary>
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="AccessGroupRequired")]
		public Guid GroupId { get ; set ; }

		/// <summary>
		/// Gets/sets the permission name.
		/// </summary>
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="AccessNameRequired")]
		[StringLength(64, ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="AccessNameLength")]
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the optional description.
		/// </summary>
		[StringLength(64, ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="AccessDescriptionLength")]
		public string Description { get ; set ; }

		/// <summary>
		/// Gets/sets if the permission is locked.
		/// </summary>
		public bool IsLocked { get ; set ; }

		/// <summary>
		/// Gets/sets the available groups.
		/// </summary>
		public IList<PermissionGroup> PermissionGroups { get ; set ; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PermissionEditModel() {
			PermissionGroups = new List<PermissionGroup>() ;
		}
	}
}