﻿using System;
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
		public async Task<IActionResult> Create([Bind("Id,Name,UserName,Password,Email,PhoneNumber,IsLock,RoleId")] AppUser appUser)
		{
			if (ModelState.IsValid)
			{
				_context.Add(appUser);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "RoleName", appUser.RoleId);
			return View(appUser);
		}

		[HttpGet("edit")]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Products == null)
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

		// GET: Admin/AppUsers/Delete/5
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