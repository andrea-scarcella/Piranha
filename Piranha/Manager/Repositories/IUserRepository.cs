using System;

namespace Piranha.Manager.Repositories
{
	/// <summary>
	/// The main user repository for the manager interface.
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// Gets the user list model.
		/// </summary>
		/// <returns>The model</returns>
		Models.UserListModel GetAll() ;

		/// <summary>
		/// Creates a new edit model.
		/// </summary>
		/// <returns>The model</returns>
		Models.UserEditModel Create() ;

		/// <summary>
		/// Refreshes the given model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The refreshed model</returns>
		Models.UserEditModel Refresh(Models.UserEditModel model) ;

		/// <summary>
		/// Gets the edit model for the user with the given id.
		/// </summary>
		/// <param name="id">The user id</param>
		/// <returns>The model</returns>
		Models.UserEditModel GetById(Guid id) ;

		/// <summary>
		/// Saves the given edit model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>If the user was saved</returns>
		bool Save(Models.UserEditModel model) ;

		/// <summary>
		/// Deletes the given edit model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>If the model was deleted</returns>
		bool Delete(Models.UserEditModel model) ;
	}
}
