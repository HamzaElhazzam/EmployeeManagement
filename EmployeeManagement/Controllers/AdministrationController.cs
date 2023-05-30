using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    [Authorize(Roles = "Owner")]
    public class AdministrationController : Controller
    {

        public RoleManager<IdentityRole> RoleManager { get; }
        public UserManager<AppUser> UserManager { get; }

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateRole(AdministarationCreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.RoleName
                };
                IdentityResult result = await RoleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(actionName: "index", controllerName: "Employee");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult ListRoles()
        {
            var roles = this.RoleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id is null)
            {
                return View("NotFound", "Please Add Role Id in the URL !");
            }
            IdentityRole role = await this.RoleManager.FindByIdAsync(id);
            if (role is null)
            {
                return View("NotFound", $"the role as {id} cannot be found ! ");
            }
            EditRoleViewModel model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = new List<string>()
                //khassni njbed la liste dyal les utilisateurs li dayrin role donc ghan instanci wahd consructor injection

            };
            foreach (var user in await UserManager.Users.ToListAsync())
            {
                //wash had user member fhad role wla la
                if (await UserManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.Email);
                }
            }
            return View(model);
        }







        [HttpPost]
        public async Task<ActionResult> Edit(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await this.RoleManager.FindByIdAsync(model.Id);
                if (role is null)
                {
                    return View("NotFound", $"the role as {model.Id} cannot be found ! ");
                }
                role.Name = model.RoleName;
                IdentityResult result = await RoleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListRoles));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }


            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> EditUsersRole(string idRole)
        {
            if (string.IsNullOrEmpty(idRole))
            {
                return View("NotFound", $"the Role Must be exist and not empty in the URL !");
            }
            var role = await this.RoleManager.FindByIdAsync(idRole);
            if (role is null)
            {
                return View("NotFound", $"the role as {idRole} cannot be found ! ");
            }
            List<EditUsersRoleViewModel> Models = new List<EditUsersRoleViewModel>();
            foreach (AppUser user in UserManager.Users.ToList())
            {
                EditUsersRoleViewModel model = new EditUsersRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = false
                };
                if (await UserManager.IsInRoleAsync(user, role.Name))
                {
                    model.IsSelected = true;
                }
                Models.Add(model);
            }
            ViewBag.RoleId = idRole;
            return View(Models);
        }
        [HttpPost]
        public async Task<ActionResult> EditUsersRole(List<EditUsersRoleViewModel> model, string idRole)
        {
            if (string.IsNullOrEmpty(idRole))
            {
                return View("NotFound", $"the Role Must be exist and not empty in the URL !");
            }
            var role = await this.RoleManager.FindByIdAsync(idRole);
            if (role is null)
            {
                return View("NotFound", $"the role as {idRole} cannot be found ! ");
            }
            for (int i = 0; i < model.Count; i++)
            {
                AppUser user = await UserManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (await UserManager.IsInRoleAsync(user, role.Name) && !model[i].IsSelected)
                {
                    result = await UserManager.RemoveFromRoleAsync(user, role.Name);
                }
                else if (!await UserManager.IsInRoleAsync(user, role.Name) && model[i].IsSelected)
                {
                    result = await UserManager.AddToRoleAsync(user, role.Name);
                }
            }
            return RedirectToAction(nameof(Edit), new { id = idRole });
        }
        [HttpPost]
        public async Task<ActionResult> DeleteRole(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);
            if(!(role is null))
            {
                var resultat = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction(nameof(ListRoles));
        }
    }
}
