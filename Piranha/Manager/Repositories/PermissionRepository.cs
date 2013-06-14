using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using AutoMapper;

namespace Piranha.Manager.Repositories
{
	/// <summary>
	/// The manager permission repository.
	/// </summary>
	public class PermissionRepository : IPermissionRepository
	{
		/// <summary>
		/// Gets the permission list model.
		/// </summary>
		/// <returns>The model</returns>
		public Models.PermissionListModel GetAll() {
			using (var db = new DataContext()) {
				var permissions = db.Permissions.Include(p => p.Group).OrderBy(p => p.Name).ToList() ;
				return new Models.PermissionListModel() {
					Permissions = Mapper.Map<IList<Entities.Permission>, IList<Models.PermissionListModel.PermissionModel>>(permissions)
				} ;
			}
		}

		/// <summary>
		/// Creates a new edit model.
		/// </summary>
		/// <returns>The model</returns>
		public Models.PermissionEditModel Create() {
			var m = new Models.PermissionEditModel() {
				Id = Guid.NewGuid()
			} ;

			using (var db = new DataContext()) {
				m.PermissionGroups = db.Groups.OrderBy(g => g.Name).Select(g => new Models.PermissionEditModel.PermissionGroup() { Id = g.Id, Name = g.Name }).ToList() ;
			}
			return m ;
		}

		/// <summary>
		/// Gets the edit model for the permission with the given id.
		/// </summary>
		/// <param name="id">The permission id</param>
		/// <returns>The model</returns>
		public Models.PermissionEditModel GetById(Guid id) {
			using (var db = new DataContext()) {
				return Mapper.Map<Entities.Permission, Models.PermissionEditModel>(
					db.Permissions.Where(p => p.Id == id).Single(), Create()) ;
			}
		}

		/// <summary>
		/// Saves the given edit model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>If the permission was saved</returns>
		public bool Save(Models.PermissionEditModel model) {
			using (var db = new DataContext()) {
				var permission = db.Permissions.Where(p => p.Id == model.Id).SingleOrDefault() ;
				if (permission == null) {
					permission = new Entities.Permission() {
						Id = model.Id
					} ;
					db.Permissions.Add(permission) ;
				}
				Mapper.Map<Models.PermissionEditModel, Entities.Permission>(model, permission) ;

				return db.SaveChanges() > 0 ;
			}
		}

		/// <summary>
		/// Deletes the given edit model.
		/// </summary>
		/// <param name="model">The model</param>
		/// <returns>If the model was deleted</returns>
		public bool Delete(Models.PermissionEditModel model) {
			using (var db = new DataContext()) {
				var permission = db.Permissions.Where(u => u.Id == model.Id).Single() ;
				db.Permissions.Remove(permission) ;

				return db.SaveChanges() > 0 ;
			}
		}
	}
}