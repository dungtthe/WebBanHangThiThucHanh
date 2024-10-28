using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebBanHang.Helpers;
using WebBanHang.Models;
using WebBanHang.Security;
using WebBanHang.ViewModel;

namespace WebBanHang.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AdminAuthorizationFilter]
	[Route("admin/user")]
	public class AppUsersController : Controller
	{
		private readonly WebDbContext _context;

		public AppUsersController(WebDbContext context)
		{
			_context = context;
		}

		[HttpGet("")]
		public async Task<IActionResult> Index()
		{
			var webDbContext = _context.AppUsers.Include(a => a.Role);
			return View(await webDbContext.ToListAsync());
		}

        // GET: Admin/AppUsers/Details/5
        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: Admin/AppUsers/Create
        [HttpGet("create")]
		public IActionResult Create()
		{
			ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "RoleName");
			return View();
		}

        // POST: Admin/AppUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,UserName,Password,Email,PhoneNumber,RoleId")] CreateAppUserViewModel appUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var checkUser = Utils.CheckTonTaiUserNameAndEmail(appUserViewModel.UserName, appUserViewModel.Email, _context);

                if (!checkUser)
                {
                    var appUser = new AppUser
                    {
                        Name = appUserViewModel.Name,
                        UserName = appUserViewModel.UserName,
                        Password = appUserViewModel.Password,
                        Email = appUserViewModel.Email,
                        PhoneNumber = appUserViewModel.PhoneNumber,
                        RoleId = appUserViewModel.RoleId,
                        IsLock = false
                    };
                    _context.Add(appUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tên tài khoản hoặc email đã tồn tại.");
                    ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "RoleName", 1);
                    return View(appUserViewModel);
                }
            }

            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "RoleName", 1);
            return View(appUserViewModel);
        }


        // GET: Admin/AppUsers/Edit/5
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

			var user = await _context.Products.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		// POST: Admin/Product/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost("edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserName,Password,PhoneNumber,IsLock,RoleId")] AppUser user)
		{
			if (id != user.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(user);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AppUserExists(user.Id))
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
			return View(user);
		}

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                _context.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(int id)
        {
            return (_context.AppUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
