using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Piranha.Entities
{
	/// <summary>
	/// The permission entity.
	/// </summary>
	[Serializable]
	public class Permission : StandardEntity<Permission>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the id of the group who is attached to the permission.
		/// </summary>
		public Guid GroupId { get ; set ; }

		/// <summary>
		/// Gets/sets the name of the permission.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the description shown in the manager interface.
		/// </summary>
		public string Description { get ; set ; }

		/// <summary>
		/// Gets/sets whether this permission can be removed or not.
		/// </summary>
		public bool IsLocked { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the group attached to the permission.
		/// </summary>
		public Group Group { get ; set ; }
		#endregion

		#region Events
		/// <summary>
		/// Executed before the permission is saved.
		/// </summary>
		/// <param name="db">The current context</param>
		/// <param name="state">The entity state</param>
		public override void OnSave(DataContext db, System.Data.EntityState state) {
			// Validate the permission name
			ValidateName() ;

			// Go ahead and save
			base.OnSave(db, state);
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Validates the name before saving.
		/// </summary>
		private void ValidateName() {
			Name = Regex.Replace(Name.ToUpper().Replace(" ", "_").Replace("Å", "A").Replace("Ä", "A").Replace("Ö", "O"),
				@"[^A-Z0-9_/]", "").Replace("__", "_") ;
		}
		#endregion
	}
}
