using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha;
using Piranha.Data;
using Piranha.Models;
using Piranha.Models.Manager.SettingModels;

using Piranha.Manager.Models;
using Piranha.Manager.Repositories;

namespace Piranha.Areas.Manager.Controllers
{
	/// <summary>
	/// Settings controller for the manager area.
	/// </summary>
    public class SettingsController : ManagerController
	{
		#region Members
		private readonly IUserRepository UserRepository = null ;
		private readonly IPermissionRepository PermissionRepository = null ;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SettingsController() : this(new UserRepository(), new PermissionRepository()) {}

		/// <summary>
		/// Creates settings controller with the given repositories.
		/// </summary>
		/// <param name="userRep">The user repository</param>
		/// <param name="permRep">The permission repository</param>
		public SettingsController(IUserRepository userRep, IPermissionRepository permRep) : base() {
			UserRepository = userRep ;
			PermissionRepository = permRep ;
		}

		#region User actions
		/// <summary>
		/// Gets the list of all users.
		/// </summary>
		/// <returns></returns>
		[Access(Function="ADMIN_USER")]
		public ActionResult UserList() {
			ViewBag.Title = @Piranha.Resources.Settings.ListTitleUsers ;

			var model = UserRepository.GetAll() ;

			// TODO: Executes the user list loaded hook, if registered
			if (WebPages.Hooks.Manager.UserListModelLoaded != null)
				WebPages.Hooks.Manager.UserListModelLoaded(this, WebPages.Manager.GetActiveMenuItem(), model) ;

            return View(@"~/Areas/Manager/Views/Settings/UserList.cshtml", model);
		}

		/// <summary>
		/// Edits or creates a new user.
		/// </summary>
		/// <param name="id">The user id</param>
		[Access(Function="ADMIN_USER")]
		public new ActionResult User(string id) {
			var model = UserRepository.Create() ;

			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingUser ;
				model = UserRepository.GetById(new Guid(id)) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewUser ;
			}

			// TODO: Executes the user list loaded hook, if registered
			if (WebPages.Hooks.Manager.UserEditModelLoaded != null)
				WebPages.Hooks.Manager.UserEditModelLoaded(this, WebPages.Manager.GetActiveMenuItem(), model) ;
	
			return View(@"~/Areas/Manager/Views/Settings/User.cshtml", model) ;
		}

		/// <summary>
		/// Saves the model
		/// </summary>
		/// <param name="em">The model</param>
		[HttpPost(), ValidateInput(false)]
		[Access(Function="ADMIN_USER")]
		public new ActionResult User(UserEditModel m) {
			ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingUser ;

			if (ModelState.IsValid) {
				try {
					UserRepository.Save(m) ;
					SuccessMessage(Piranha.Resources.Settings.MessageUserSaved, true) ;
					return RedirectToAction("user", new { id = m.Id }) ;
				} catch {
					ErrorMessage(Piranha.Resources.Settings.MessageUserNotSaved) ;
				}
			}
			return View(@"~/Areas/Manager/Views/Settings/User.cshtml", UserRepository.Refresh(m)) ;
		}

		/// <summary>
		/// Deletes the specified user
		/// </summary>
		/// <param name="id">The user id</param>
		[Access(Function="ADMIN_USER")]
		public ActionResult DeleteUser(string id) {
			var model = UserRepository.GetById(new Guid(id)) ;

			try {
				if (UserRepository.Delete(model))
					SuccessMessage(Piranha.Resources.Settings.MessageUserDeleted, true) ;
				else ErrorMessage(Piranha.Resources.Settings.MessageUserNotDeleted, true) ;
			} catch {
				ErrorMessage(Piranha.Resources.Settings.MessageUserNotDeleted, true) ;
			}
			return RedirectToAction("userlist") ;
		}

		/// <summary>
		/// Generates a new random password for the given user.
		/// </summary>
		/// <param name="id">The user id</param>
		[Access(Function="ADMIN_USER")]
		public ActionResult GeneratePassword(string id) {
			SysUserPassword password = SysUserPassword.GetSingle(new Guid(id)) ;
			string newpwd = SysUserPassword.GeneratePassword() ;

			password.Password = password.PasswordConfirm = newpwd ;
			password.Save() ;
			InformationMessage(Piranha.Resources.Settings.MessageNewPassword + newpwd) ;

			return User(id) ;
		}
		#endregion

		#region Group actions
		/// <summary>
		/// Gets the group list.
		/// </summary>
		[Access(Function="ADMIN_GROUP")]
        public ActionResult GroupList() {
            return View(@"~/Areas/Manager/Views/Settings/GroupList.cshtml", GroupListModel.Get());
        }

		/// <summary>
		/// Edits or creates a new group
		/// </summary>
		/// <param name="id">The group id</param>
		[Access(Function="ADMIN_GROUP")]
		public ActionResult Group(string id) {
			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingGroup ;
				return View(@"~/Areas/Manager/Views/Settings/Group.cshtml", GroupEditModel.GetById(new Guid(id))) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewGroup ;
				return View(@"~/Areas/Manager/Views/Settings/Group.cshtml", new GroupEditModel()) ;
			}
		}

		/// <summary>
		/// Saves the group
		/// </summary>
		/// <param name="gd">The model</param>
		[HttpPost(), ValidateInput(false)]
		[Access(Function="ADMIN_GROUP")]
		public ActionResult Group(GroupEditModel gm) {
			if (gm.Group.IsNew)
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewGroup ;
			else ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingGroup ;

			if (ModelState.IsValid) {
				try {
					if (gm.SaveAll()) {
						ModelState.Clear() ;
						ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingGroup ;
						SuccessMessage(Piranha.Resources.Settings.MessageGroupSaved) ;
					} else ErrorMessage(Piranha.Resources.Settings.MessageGroupNotSaved) ;
				} catch (Exception e) {
					ErrorMessage(e.ToString()) ;
				}
			}
			gm.Refresh() ;
			return View(@"~/Areas/Manager/Views/Settings/Group.cshtml", gm) ;
		}

		/// <summary>
		/// Deletes the specified group
		/// </summary>
		/// <param name="id">The group id</param>
		[Access(Function="ADMIN_GROUP")]
		public ActionResult DeleteGroup(string id) {
			GroupEditModel gm = GroupEditModel.GetById(new Guid(id)) ;
			
			ViewBag.SelectedTab = "groups" ;
			if (gm.DeleteAll())
				SuccessMessage(Piranha.Resources.Settings.MessageGroupDeleted) ;
			else ErrorMessage(Piranha.Resources.Settings.MessageGroupNotDeleted) ;
			
			return GroupList() ;
		}
		#endregion

		#region Permission actions
		/// <summary>
		/// Gets the access list.
		/// </summary>
		[Access(Function="ADMIN_ACCESS")]
        public ActionResult AccessList() {
			var model = PermissionRepository.GetAll() ;

            return View(@"~/Areas/Manager/Views/Settings/AccessList.cshtml", model);
        }

		/// <summary>
		/// Edits or creates a new group
		/// </summary>
		/// <param name="id">The group id</param>
		[Access(Function="ADMIN_ACCESS")]
		public ActionResult Access(string id) {
			var model = PermissionRepository.Create() ;

			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;
				model = PermissionRepository.GetById(new Guid(id)) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewAccess ;
			}
			return View(@"~/Areas/Manager/Views/Settings/Access.cshtml", model) ;
		}

		/// <summary>
		/// Saves the access
		/// </summary>
		/// <param name="gd">The model</param>
		[HttpPost()]
		[Access(Function="ADMIN_ACCESS")]
		public ActionResult Access(PermissionEditModel m) {
			ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingAccess ;

			if (ModelState.IsValid) {
				try {
					PermissionRepository.Save(m) ;
					SuccessMessage(Piranha.Resources.Settings.MessageAccessSaved, true) ;
					return RedirectToAction("access", new { id = m.Id }) ;
				} catch {
					ErrorMessage(Piranha.Resources.Settings.MessageAccessNotSaved) ;
				}
			}
			return View(@"~/Areas/Manager/Views/Settings/Access.cshtml", PermissionRepository.Refresh(m)) ;
		}

		/// <summary>
		/// Deletes the specified group
		/// </summary>
		/// <param name="id">The access id</param>
		[Access(Function="ADMIN_ACCESS")]
		public ActionResult DeleteAccess(string id) {
			var model = PermissionRepository.GetById(new Guid(id)) ;

			try {
				if (PermissionRepository.Delete(model))
					SuccessMessage(Piranha.Resources.Settings.MessageAccessDeleted, true) ;
				else ErrorMessage(Piranha.Resources.Settings.MessageAccessNotDeleted, true) ;
			} catch {
				ErrorMessage(Piranha.Resources.Settings.MessageAccessNotDeleted, true) ;
			}
			return RedirectToAction("accesslist") ;
		}
		#endregion

		#region Param actions
		/// <summary>
		/// Gets the param list.
		/// </summary>
		[Access(Function="ADMIN_PARAM")]
        public ActionResult ParamList() {
            return View(@"~/Areas/Manager/Views/Settings/ParamList.cshtml", ParamListModel.Get());
        }

		/// <summary>
		/// Edits or creates a new parameter
		/// </summary>
		/// <param name="id">Parameter id</param>
		[Access(Function="ADMIN_PARAM")]
		public ActionResult Param(string id) {
			if (!String.IsNullOrEmpty(id)) {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingParam ;
				return View(@"~/Areas/Manager/Views/Settings/Param.cshtml", ParamEditModel.GetById(new Guid(id))) ;
			} else {
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewParam ;
				return View(@"~/Areas/Manager/Views/Settings/Param.cshtml", new ParamEditModel()) ;
			}
		}

		/// <summary>
		/// Edits or creates a new parameter
		/// </summary>
		/// <param name="id">Parameter id</param>
		[HttpPost()]
		[Access(Function="ADMIN_PARAM")]
		public ActionResult Param(ParamEditModel pm) {
			if (pm.Param.IsNew)
				ViewBag.Title = Piranha.Resources.Settings.EditTitleNewParam ;
			else ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingParam ;

			if (ModelState.IsValid) {
				try {
					if (pm.SaveAll()) {
						ModelState.Clear() ;
						ViewBag.Title = Piranha.Resources.Settings.EditTitleExistingParam ;
						SuccessMessage(Piranha.Resources.Settings.MessageParamSaved) ;
					} else ErrorMessage(Piranha.Resources.Settings.MessageParamNotSaved) ;
				} catch (Exception e) {
					ErrorMessage(e.ToString()) ;
				}
			}
			return View(@"~/Areas/Manager/Views/Settings/Param.cshtml", pm) ;
		}


		/// <summary>
		/// Deletes the specified param
		/// </summary>
		/// <param name="id">The param</param>
		[Access(Function="ADMIN_PARAM")]
		public ActionResult DeleteParam(string id) {
			ParamEditModel pm = ParamEditModel.GetById(new Guid(id)) ;
			
			ViewBag.SelectedTab = "params" ;
			if (pm.DeleteAll())
				SuccessMessage(Piranha.Resources.Settings.MessageParamDeleted) ;
			else ErrorMessage(Piranha.Resources.Settings.MessageParamNotDeleted) ;

			return ParamList() ;
		}
		#endregion
	}
}
