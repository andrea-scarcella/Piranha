using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Piranha.Extend;

namespace Piranha.Manager.Binders
{
	internal class UserEditModelBinder : DefaultModelBinder
	{
		/// <summary>
		/// Extend the default binder so that html strings can be fetched from the post.
		/// </summary>
		/// <param name="controllerContext">Controller context</param>
		/// <param name="bindingContext">Binding context</param>
		/// <returns>The page edit model</returns>
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			var model = (Models.UserEditModel)base.BindModel(controllerContext, bindingContext) ;

			// Allow HtmlString extensions
			model.Extensions.Each((i, m) => {
				if (m.Body is HtmlString) {
					bindingContext.ModelState.Remove("Extensions[" + i +"].Body") ;
					m.Body = ExtensionManager.Current.CreateInstance(m.Type,
 						bindingContext.ValueProvider.GetUnvalidatedValue("Extensions[" + i +"].Body").AttemptedValue) ;
				}
			}) ;
			return model ;
		}
	}
}