using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Helpers;
using WebBanHang.Models;
using WebBanHang.Security;
using WebBanHang.ViewModel;

namespace WebBanHang.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AdminAuthorizationFilter]
	[Route("admin/product")]
	public class ProductController : Controller
	{
		private readonly WebDbContext _context;

		public ProductController(WebDbContext context)
		{
			_context = context;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			var webDbContext = _context.Products.Include(p => p.Category);
			return View(await webDbContext.ToListAsync());
		}

		// GET: Admin/Product/Details/5
		[HttpGet("details")]
		public async Task<IActionResult> Details(int? id)
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

		[HttpGet("create")]
		public IActionResult Create()
		{
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
			return View();
		}

		[HttpPost("create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Price,Quantity,Images,CategoryId")] Product product)
		{
			if (ModelState.IsValid)
			{
				_context.Add(product);
				await _context.SaveChangesAsync();
				TempData["Message"] = "Thêm sản phẩm " + product.Name + " thành công";
				return RedirectToAction(nameof(Index));
			}
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
			return View(product);
		}



        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
			return View(product);
		}

		// POST: Admin/Product/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost("edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity,Images,CategoryId")] Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(product);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductExists(product.Id))
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
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
			return View(product);
		}

		// GET: Admin/Product/Delete/5

		[HttpGet("delete")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (_context.Products == null)
			{
				return Problem("Không có sản phẩm nào");
			}
			var product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				_context.Products.Remove(product);
				TempData["Message"] = "Xóa sản phẩm " + product.Name + " thành công.";
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// POST: Admin/Product/Delete/5
		/*[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
		}*/

		private bool ProductExists(int id)
		{
			return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
		}





        [HttpGet("uploadphoto")]
        public IActionResult UploadPhoto(int id)
        {
            var product = _context.Products.Where(e => e.Id == id)
                .FirstOrDefault();
            if (product == null)
            {
                return NotFound("Không có sản phẩm");
            }
            ViewData["product"] = product;
            return View(new UploadOneFile());
        }


        [HttpPost("uploadphoto"), ActionName("UploadPhoto")]
        public async Task<IActionResult> UploadPhotoAsync(int id, [Bind("FileUpload")] UploadOneFile f)
        {
            var product = _context.Products.Where(e => e.Id == id)
                .FirstOrDefault();

            if (product == null)
            {
                return NotFound("Không có sản phẩm");
            }

            ViewData["product"] = product;

            if (f != null)
            {
                // fileName random
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())
                     + Path.GetExtension(f.FileUpload.FileName);
                var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                var filePath = Path.Combine(wwwRootPath, fileName);

                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    await f.FileUpload.CopyToAsync(filestream);
                }


				var imgs = Utils.AddPhotoForProduct(fileName, product.Images);
				product.Images = imgs;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Edit), new { id = id });
        }



        [HttpDelete("deleteimage")]
        public IActionResult DeleteImage([FromBody] DeleteImageRequest request)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == request.ProductId);
            if (string.IsNullOrEmpty(request.FileName) || product == null)
            {
                return BadRequest("Có lỗi xảy ra.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", request.FileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

			product.Images=Utils.RemovePhotoForProduct(request.FileName, product.Images);
			_context.SaveChanges();
			
            return Ok(new { message = "Ảnh đã được xóa thành công." });
        }

		//bổ trợ xóa ảnh
        public class DeleteImageRequest
        {
            public string FileName { get; set; }
            public int ProductId { get; set; }
        }
    }
}