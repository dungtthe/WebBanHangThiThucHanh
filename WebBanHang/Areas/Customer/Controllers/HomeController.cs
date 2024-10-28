using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Controllers
{
    [Area("customer")]
    [Route("")]
    public class HomeController : Controller
    {

        private readonly WebDbContext _context;
        public HomeController(WebDbContext context)
        {
            this._context = context;
        }


        [Route("")]
        [Route("home")]
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            return View(products);
        }
    }
}
