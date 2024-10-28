using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		[HttpGet("ChiTietSanPham")]
		public async Task<IActionResult> ChiTietSanPham(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products
				.Include(p => p.Category)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}
	}
}