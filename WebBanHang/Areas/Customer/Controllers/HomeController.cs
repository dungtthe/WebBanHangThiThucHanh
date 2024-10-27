using Microsoft.AspNetCore.Mvc;

namespace WebBanHang.Areas.Customer.Controllers
{
    [Area("customer")]
    [Route("")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
