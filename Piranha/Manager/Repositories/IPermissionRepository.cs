using System;
using System.Collections.Generic;

namespace Piranha.Manager.Repositories
{
	public interface IPermissionRepository
	{
		Models.PermissionListModel GetAll() ;
		Models.PermissionEditModel Create() ;
		Models.PermissionEditModel GetById(Guid id) ;
		bool Save(Models.PermissionEditModel model) ;
		bool Delete(Models.PermissionEditModel model) ;
	}
}
