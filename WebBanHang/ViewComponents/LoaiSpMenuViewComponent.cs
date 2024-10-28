using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;

namespace WebBanHang.ViewComponents
{
    public class LoaiSpMenuViewComponent:ViewComponent
    {
        private readonly WebDbContext _context;
        public LoaiSpMenuViewComponent(WebDbContext context)
        {
            this._context = context;
        }


        public IViewComponentResult Invoke()
        {
            var listloaisp = _context.Categories.ToList();
            return View(listloaisp);
        }
    }
}
