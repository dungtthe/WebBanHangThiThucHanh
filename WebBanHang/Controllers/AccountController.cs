using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebBanHang.Helpers;
using WebBanHang.Models;
using WebBanHang.Security;

namespace WebBanHang.Controllers
{
    public class AccountController : Controller
    {
        private readonly WebDbContext _context;
        public AccountController(WebDbContext context)
        {
            this._context = context;
        }

        [TempData]
        public string ErrorMessage { get; set; }
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/login")]
        public IActionResult Login(AppUser user)
        {
            SessionHelpers.Clear(HttpContext);
            var u = _context.AppUsers
                                   .Include(x => x.Role)
                                   .FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (u != null)
            {
                SessionHelpers.SetUserId(HttpContext, u.Id);
                SessionHelpers.SetRoleName(HttpContext, u.Role.RoleName);
                if (u.Role.RoleName == RolesConst.Admin)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }
            }
            ErrorMessage = "Thông tin đăng nhập không đúng";
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Logout()
        {
            var roleName = SessionHelpers.GetRoleName(HttpContext);
            SessionHelpers.Clear(HttpContext);
            if (roleName == RolesConst.Admin)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }


    

        [HttpPost("/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name,UserName,Password,Email")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Register", "Account");
        }
    }
}
