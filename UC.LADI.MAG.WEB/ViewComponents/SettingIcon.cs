using Microsoft.AspNetCore.Mvc;
using UC.LADI.MAG.WEB.ViewModels.Shared;

namespace UC.LADI.MAG.WEB.ViewComponents
{
	[ViewComponent]
	public class SettingIcon : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}


