using Microsoft.AspNetCore.Mvc;

namespace UC.LADI.MAG.WEB.ViewComponents
{
    [ViewComponent]
    public class ScriptCkEditor : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
