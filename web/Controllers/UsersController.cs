using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Entities;
using web.Models.Users;

namespace web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : Controller
    {
        readonly MyDbContext _dbContext;
        readonly UserManager<MyUser> _userManager;

        public UsersController(MyDbContext dbContext, UserManager<MyUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userTasks = _userManager.Users
                .AsEnumerable()
                .Select(async x => new UserListItem {
                    UserName = x.UserName,
                    DisplayName = x.DisplayName,
                    IsAdmin = await _userManager.IsInRoleAsync(x, Roles.Admin)
                });

            var users = await Task.WhenAll(userTasks);

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return NotFound();
            }

            return View(new UserDetails {
                EmailAddress = user.Email,
                Id = user.Id,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                IsAdmin = await _userManager.IsInRoleAsync(user, Roles.Admin)
            });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAdmin(string userName, bool admin)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            if(admin)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin);
            } else
            {
                await _userManager.RemoveFromRoleAsync(user, Roles.Admin);
            }

            return Json(admin);
        }
    }
}
