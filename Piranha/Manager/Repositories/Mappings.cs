using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

namespace Piranha.Manager.Repositories
{
	/// <summary>
	/// Configures AutoMapping for the manager interface
	/// </summary>
	internal static class Mappings
	{
		/// <summary>
		/// Intitializes the mappings.
		/// </summary>
		public static void Init() {
			#region User
			Mapper.CreateMap<Entities.User, Models.UserEditModel>()
				.ForMember(m => m.GroupId, o => o.MapFrom(u => u.GroupId.HasValue ? u.GroupId.Value : Guid.Empty))
				.ForMember(m => m.Extensions, o => o.Ignore())
				.ForMember(m => m.UserGroups, o => o.Ignore())
				.ForMember(m => m.PasswordConfirm, o => o.Ignore()) ;
			Mapper.CreateMap<Models.UserEditModel, Entities.User>()
				.ForMember(u => u.Id, o => o.Ignore())
				.ForMember(u => u.APIKey, o => o.Ignore())
				.ForMember(u => u.Created, o => o.Ignore())
				.ForMember(u => u.CreatedById, o => o.Ignore())
				.ForMember(u => u.Updated, o => o.Ignore())
				.ForMember(u => u.UpdatedById, o => o.Ignore())
				.ForMember(u => u.LastLogin, o => o.Ignore())
				.ForMember(u => u.PreviousLogin, o => o.Ignore())
				.ForMember(u => u.Culture, o => o.Ignore())
				.ForMember(u => u.Group, o => o.Ignore())
				.ForMember(u => u.Extensions, o => o.Ignore())
				.ForMember(u => u.Password, o => o.Ignore());
			#endregion

			#region Permission
			Mapper.CreateMap<Entities.Permission, Models.PermissionListModel.PermissionModel>()
				.ForMember(m => m.GroupName, o => o.MapFrom(u => u.Group.Name)) ;
			Mapper.CreateMap<Entities.Permission, Models.PermissionEditModel>()
				.ForMember(m => m.PermissionGroups, o => o.Ignore()) ;
			Mapper.CreateMap<Models.PermissionEditModel, Entities.Permission>()
				.ForMember(p => p.Group, o => o.Ignore())
				.ForMember(p => p.Created, o => o.Ignore())
				.ForMember(p => p.CreatedBy, o => o.Ignore())
				.ForMember(p => p.CreatedById, o => o.Ignore())
				.ForMember(p => p.Updated, o => o.Ignore())
				.ForMember(p => p.UpdatedBy, o => o.Ignore())
				.ForMember(p => p.UpdatedById, o => o.Ignore()) ;
			#endregion

			Mapper.AssertConfigurationIsValid() ;
		}
	}
}