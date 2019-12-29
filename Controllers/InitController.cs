using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace pspbe.Controllers
{
    [Authorize]
    public class InitController : Controller
    {
        private RoleManager<IdentityRole> _roleManager { get; }
        private UserManager<IdentityUser> _userManager { get; }
        public InitController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            // If root role already exists in the system, return 404 
            if (await _roleManager.RoleExistsAsync("root"))
            {
                var notFoundObject = new NotFoundObjectResult(this.NotFound());
                return NotFound();
            }
            // Create root and admin roles and assign them to the current user
            List<string> rolesList = new List<string>();
            IdentityResult rootResult = await _roleManager.CreateAsync(new IdentityRole("root"));
                if (rootResult.Succeeded == true)
                {
                    rolesList.Add("root");
                    IdentityResult adminResult = await _roleManager.CreateAsync(new IdentityRole("admin"));
                    if (adminResult.Succeeded == true)
                    {
                        rolesList.Add("admin");
                        var currentUser = await _userManager.GetUserAsync(User);
                        await _userManager.AddToRolesAsync(currentUser, rolesList);
                    }
                }
            return RedirectToAction("Index", "Home");
        }
    }
}