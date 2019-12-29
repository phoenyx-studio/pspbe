using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using pspbe.Data;

namespace pspbe.Controllers
{
    [Authorize(Roles = "root")]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users;
            return View(users);
        }
        public async Task<IActionResult> SetRoot() 
        {
            var user = await _userManager.FindByEmailAsync("yurko_ne@ukr.net");
            await _userManager.AddToRoleAsync(user, "root");
            return RedirectToAction("Index", "User");
        }
        public async Task<IActionResult> SetName() 
        {
            var user = await _userManager.FindByEmailAsync("yurko_ne@ukr.net");
            await _userManager.SetUserNameAsync(user, "yurko_ne@ukr.net");
            return RedirectToAction("Index", "User");
        }
    }
}