
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace pspbe.Controllers
{
public class RoleController : Controller
{
    RoleManager<IdentityRole> _roleManager;
    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var roles = _roleManager.Roles;
        //ViewBag.Roles = roles;
        return View(roles);
    }
    public async Task<IActionResult> Create()
    {
        var role = await _roleManager.CreateAsync(new IdentityRole("root"));
        return RedirectToAction("Index", "Role");
    }
}
}