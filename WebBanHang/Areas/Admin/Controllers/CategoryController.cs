using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Security;

namespace WebBanHang.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AdminAuthorizationFilter]
	[Route("admin/category")]
	public class CategoryController : Controller
	{
		private readonly WebDbContext _context;

		[TempData]
		public string Message { get; set; }

		public CategoryController(WebDbContext context)
		{
			_context = context;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			return _context.Categories != null ?
						View(await _context.Categories.ToListAsync()) :
						Problem("Entity set 'WebDbContext.Categories'  is null.");
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Categories == null)
			{
				return NotFound();
			}

			var category = await _context.Categories
				.FirstOrDefaultAsync(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		[HttpGet("create")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost("create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
		{
			if (ModelState.IsValid)
			{
				_context.Add(category);
				await _context.SaveChangesAsync();
				TempData["Message"] = "Thêm danh mục " + category.Name + " thành công";
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		[HttpGet("edit")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Categories == null)
			{
				return NotFound();
			}

			var category = await _context.Categories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost("edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
		{
			if (id != category.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(category);
					await _context.SaveChangesAsync();
					TempData["Message"] = "Cập nhật danh mục thành công";
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CategoryExists(category.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		[HttpGet("delete")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Categories == null)
			{
				return NotFound();
			}

			var category = await _context.Categories
				.FirstOrDefaultAsync(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}

			var listProduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == category.Id);
			if (listProduct == null)
			{
				TempData["Message"] = "Xóa thành công danh mục " + category.Name;
				_context.Categories.Remove(category);
				await _context.SaveChangesAsync();
			}
			else
			{
				TempData["Message"] = "Không thể xóa vì có sản phẩm thuộc danh mục này";
			}
			return RedirectToAction(nameof(Index));
		}

		private bool CategoryExists(int id)
		{
			return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}