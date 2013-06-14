using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// Manager list model for users.
	/// </summary>
	public class UserListModel
	{
		/// <summary>
		/// User model.
		/// </summary>
		public class UserModel {
			/// <summary>
			/// Gets/sets the user id.
			/// </summary>
			public Guid Id { get ; set ; }

			/// <summary>
			/// Gets/sets the group id.
			/// </summary>
			public Guid GroupId { get ; set ; }

			/// <summary>
			/// Gets/sets the login name.
			/// </summary>
			public string Login { get ; set ; }

			/// <summary>
			/// Gets/sets the full name.
			/// </summary>
			public string Name { get ; set ; }

			/// <summary>
			/// Gets/sets the group name.
			/// </summary>
			public string GroupName { get ; set ; }

			/// <summary>
			/// Gets/sets the optional gravatar url.
			/// </summary>
			public string GravatarUrl { get ; set ; }

			/// <summary>
			/// Gets/sets the date the user was created.
			/// </summary>
			public DateTime Created { get ; set ; }

			/// <summary>
			/// Gets/sets the date the user was last updated.
			/// </summary>
			public DateTime Updated { get ; set ; }
		}

		/// <summary>
		/// Gets/sets the currently available users.
		/// </summary>
		public IList<UserModel> Users { get ; set ; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public UserListModel() {
			Users = new List<UserModel>() ;
		}
	}
}