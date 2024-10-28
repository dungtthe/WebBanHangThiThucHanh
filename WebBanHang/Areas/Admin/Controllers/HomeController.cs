using Microsoft.AspNetCore.Mvc;
using WebBanHang.Security;

namespace WebBanHang.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("admin")]
    [AdminAuthorizationFilter]
    public class HomeController : Controller
    {

        [Route("")]
        [Route("home")]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "AppUsers");
        }
    }
}
