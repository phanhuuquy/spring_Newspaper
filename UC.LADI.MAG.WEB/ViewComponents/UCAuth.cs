using Microsoft.AspNetCore.Mvc;


namespace UC.LADI.MAG.WEB.ViewComponents
{
    [ViewComponent]
    public class UCAuth : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public UCAuth(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}


