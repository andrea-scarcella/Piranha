using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Piranha.Manager.Models
{
	/// <summary>
	/// Manager edit model for users.
	/// </summary>
	public class UserEditModel
	{
		/// <summary>
		/// The groups available for a user.
		/// </summary>
		public class UserGroup {
			public Guid Id { get ; set ; }
			public string Name { get ; set ; }
		}

		/// <summary>
		/// Gets/sets the user id.
		/// </summary>
		public Guid Id { get ; set ; }

		/// <summary>
		/// Gets/sets the login name.
		/// </summary>
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="LoginRequired")]
		[StringLength(64, ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="LoginLength")]
		public string Login { get ; set ; }

		/// <summary>
		/// Gets/sets the firstname.
		/// </summary>
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="FirstnameRequired")]
		[StringLength(128, ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="FirstnameLength")]
		public string Firstname { get ; set ; }

		/// <summary>
		/// Gets/sets the surname.
		/// </summary>
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="SurnameRequired")]
		[StringLength(128, ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="SurnameLength")]
		public string Surname { get ; set ; }

		/// <summary>
		/// Gets/sets the email address.
		/// </summary>
		[Required(ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="EmailRequired")]
		[StringLength(128, ErrorMessageResourceType=typeof(Piranha.Resources.Settings), ErrorMessageResourceName="EmailLength")]
		public string Email { get ; set ; }

		/// <summary>
		/// Gets/sets the group id.
		/// </summary>
		public Guid GroupId { get ; set ; }

		/// <summary>
		/// Gets/sets whether or not the user is locked.
		/// </summary>
		public bool IsLocked { get ; set ; }

		/// <summary>
		/// Gets/sets the optional end date for the user lock.
		/// </summary>
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? LockedUntil { get ; set ; }

		/// <summary>
		/// Gets/sets the password.
		/// </summary>
		public string Password { get ; set ; }

		/// <summary>
		/// Gets/sets the password confirmation, needs to be the same as the password.
		/// </summary>
		[Compare("Password", ErrorMessage="Lösenorden matchar inte.")]
		public string PasswordConfirm { get ; set ; }

		/// <summary>
		/// Gets/sets the available extensions.
		/// </summary>
		public IList<Entities.Extension> Extensions { get ; set ; }

		/// <summary>
		/// Gets/sets the groups available.
		/// </summary>
		public IList<UserGroup> UserGroups { get ; set ; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public UserEditModel() {
			Extensions = new List<Entities.Extension>() ;
			UserGroups = new List<UserGroup>() ;
		}
	}
}