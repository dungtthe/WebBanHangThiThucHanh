using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WebBanHang.ViewModel;

namespace WebBanHang.Areas.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly WebDbContext _context;

        public ProductAPIController(WebDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            var products = _context.Products
                                   .Include(p => p.Category)
                                   .Select(p => new ProductViewModel
                                   {
                                       Id = p.Id,
                                       Name = p.Name,
                                       Price = p.Price,
                                       Image = p.Images,
                                       CategoryId = p.CategoryId,
                                       CategoryName = p.Category != null ? p.Category.Name : null
                                   })
                                   .ToList();

            return products;
        }

        [HttpGet("{categoryid}")]
        public IEnumerable<ProductViewModel> GetProductsByCategory(int categoryid)
        {
            var products = _context.Products
                                   .Include(p => p.Category)
                                   .Where(p => p.CategoryId == categoryid)
                                   .Select(p => new ProductViewModel
                                   {
                                       Id = p.Id,
                                       Name = p.Name,
                                       Price = p.Price,
                                       Image = p.Images,
                                       CategoryId = p.CategoryId,
                                       CategoryName = p.Category != null ? p.Category.Name : null
                                   })
                                   .ToList();

            return products;
        }
    }
}
