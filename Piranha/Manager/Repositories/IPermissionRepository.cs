using System;
using System.Collections.Generic;

namespace Piranha.Manager.Repositories
{
	/// <summary>
	/// The manager permission repository.
	/// </summary>
	public interface IPermissionRepository
	{
		/// <summary>
		/// Gets the permission list model.
		/// </summary>
		/// <returns>The model</returns>
		Models.PermissionListModel GetAll() ;

		/// <summary>
		/// Creates a new edit model.
		/// </summary>
		/// <returns>The model</returns>
		Models.PermissionEditModel Create() ;

		/// <summary>
		/// Refreshes the given model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The refreshed model</returns>
		Models.PermissionEditModel Refresh(Models.PermissionEditModel model) ;

		/// <summary>
		/// Gets the edit model for the permission with the given id.
		/// </summary>
		/// <param name="id">The permission id</param>
		/// <returns>The model</returns>
		Models.PermissionEditModel GetById(Guid id) ;

		/// <summary>
		/// Saves the given edit model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>If the user was saved</returns>
		bool Save(Models.PermissionEditModel model) ;

		/// <summary>
		/// Deletes the given edit model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>If the model was deleted</returns>
		bool Delete(Models.PermissionEditModel model) ;
	}
}
