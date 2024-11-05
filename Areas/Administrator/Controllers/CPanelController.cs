using CMSpro1.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRS.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize]
    public class CPanelController : Controller
    {

        #region CodeConfiguration
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public CPanelController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
        #region Roles
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RolesList()
        {
            return View(_roleManager.Roles);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(CreateViewModel NewRole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = NewRole.RoleName
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(NewRole);
            }
            return View(NewRole);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = new IdentityRole();
            role = await _roleManager.FindByIdAsync(id);
            EditViewModel editViewModel = new EditViewModel()
            {
                RoleName = role.Name,
                Roleid = role.Id
            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name!))
                {
                    editViewModel.Users.Add(user.UserName);
                }
            }
            return View(editViewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Roleid);
                if (role == null)
                {
                    return RedirectToAction("NotFound", "Account");
                }
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList", "Account");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);

        }
        [AllowAnonymous]
        public IActionResult NotFound()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserInRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("NotFound");
            }
            List<UserRoleViewModel> Model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                UserRoleViewModel NewUser = new UserRoleViewModel()
                {
                    UserName = user.UserName,
                    UserId = user.Id,
                };
                if (await _userManager.IsInRoleAsync(user, role.Name!))
                {
                    NewUser.IsSelected = true;
                }
                else
                {
                    NewUser.IsSelected = false;
                }
                Model.Add(NewUser);
            }
            return View(Model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            IdentityUser user = new IdentityUser();
            if (ModelState.IsValid)
            {
                IdentityResult r = new IdentityResult();
                for (int i = 0; i < model.Count; i++)
                {
                    user = await _userManager.FindByIdAsync(model[i].UserId);
                    if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                    {
                        r = await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        r = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        continue;
                    }
                }
                return RedirectToAction("RolesList");
            }
            return RedirectToAction("RolesList");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = new IdentityRole();
            role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                return View(role);
            }
            return RedirectToAction("RolesList");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string Role , string id)
        {
            IdentityRole role = new IdentityRole();
            role = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);
            return RedirectToAction("RolesList");
        }




        #endregion
    }
}
