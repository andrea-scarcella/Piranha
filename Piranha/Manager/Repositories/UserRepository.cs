using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using AutoMapper;

namespace Piranha.Manager.Repositories
{
	/// <summary>
	/// The manager user repository.
	/// </summary>
	public class UserRepository : IUserRepository
	{
		/// <summary>
		/// Gets the user list model.
		/// </summary>
		/// <returns>The model</returns>
		public Models.UserListModel GetAll() {
			using (var db = new DataContext()) {
				var users = db.Users.Include(u => u.Group).OrderBy(u => u.Login).ToList() ;
				var model = new Models.UserListModel() ;

				foreach (var user in users) {
					model.Users.Add(new Models.UserListModel.UserModel() {
						Id = user.Id,
						GroupId = user.GroupId.Value,
						Login = user.Login,
						Name = user.Firstname + " " + user.Surname,
						GroupName = user.Group.Name,
						GravatarUrl = GenerateGravatar(user.Email),
						Created = user.Created,
						Updated = user.Updated
					}) ;
				}
				return model ;
			}
		}

		/// <summary>
		/// Creates a new edit model.
		/// </summary>
		/// <returns>The model</returns>
		public Models.UserEditModel Create() {
			var m = new Models.UserEditModel() {
				Id = Guid.NewGuid()
			} ;
			LoadMetaData(m) ;

			return m ;
		}

		/// <summary>
		/// Gets the edit model for the user with the given id.
		/// </summary>
		/// <param name="id">The user id</param>
		/// <returns>The model</returns>
		public Models.UserEditModel GetById(Guid id) {
			using (var db = new DataContext()) {
				return Mapper.Map<Entities.User, Models.UserEditModel>(
					db.Users.Where(u => u.Id == id).Single(), Create()) ;
			}
		}

		/// <summary>
		/// Refreshes the given model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>The refreshed model</returns>
		public Models.UserEditModel Refresh(Models.UserEditModel model) {
			LoadMetaData(model) ;

			return model ;
		}

		/// <summary>
		/// Saves the given edit model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>If the user was saved</returns>
		public bool Save(Models.UserEditModel model) {
			using (var db = new DataContext()) {
				var user = db.Users.Where(u => u.Id == model.Id).SingleOrDefault() ;
				if (user == null) {
					user = new Entities.User() {
						Id = model.Id
					} ;
					db.Users.Add(user) ;
				}
				Mapper.Map<Models.UserEditModel, Entities.User>(model, user) ;

				if (!String.IsNullOrEmpty(model.Password))
					user.Password = Piranha.Models.SysUser.Encrypt(model.Password) ;

				return db.SaveChanges() > 0 ;
			}
		}

		/// <summary>
		/// Deletes the given edit model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>If the model was deleted</returns>
		public bool Delete(Models.UserEditModel model) {
			using (var db = new DataContext()) {
				var user = db.Users.Where(u => u.Id == model.Id).Single() ;
				db.Users.Remove(user) ;

				return db.SaveChanges() > 0 ;
			}
		}

		#region Private methods
		/// <summary>
		/// Loads all meta data for the model.
		/// </summary>
		/// <param name="model">The model</param>
		private void LoadMetaData(Models.UserEditModel model) {
			using (var db = new DataContext()) {
				model.UserGroups = db.Groups.OrderBy(g => g.Name).Select(g => new Models.UserEditModel.UserGroup() { Id = g.Id, Name = g.Name }).ToList() ;
			}
		}

		/// <summary>
		/// Generates the gravatar url for the given email.
		/// </summary>
		/// <param name="email">The email address</param>
		/// <returns>The gravatar url</returns>
		private string GenerateGravatar(string email) {
			if (!String.IsNullOrEmpty(email)) {
				var md5 = new MD5CryptoServiceProvider();

				var encoder = new UTF8Encoding();
				var hash = new MD5CryptoServiceProvider();
				var bytes = hash.ComputeHash(encoder.GetBytes(email));

				var sb = new StringBuilder(bytes.Length * 2);
				for (int n = 0; n < bytes.Length; n++) {
					sb.Append(bytes[n].ToString("X2"));
				}
				return "http://www.gravatar.com/avatar/" + sb.ToString().ToLower() + "?s=30" ;
			}
			return "" ;
		}
		#endregion
	}
}