using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
 using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager< AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //string fullname = GenerateUserName(model.FirstName, model.LastName);
                //hnaya drt instance l class dyali li kat presonti liya DB bash n'affecti liha les propriété
                AppUser user = new AppUser { UserName = model.Email, Email = model.Email , FirstName= model.FirstName , LastName = model.LastName , Age=model.Age };
                //hna 3yetna lclass UserManager bash dkhel lina wahd user au niveau dyal DB ou affectinaha lwahd var bash nchufu wahd true wla false
                var result = await userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    
                    //kangolih ila jab lah had user li dar register endo f role owner ou howa braso mssjel ya3ni authentifier sir dih La liste dyal users ou rah anchufu dak jdid dakhl f la liste dyal users
                    if (User.IsInRole("Owner") && signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("ListUsers");
                    }
                    //connecter
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "Employee");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        //first name ou lastname 3endna hna howa username donc ghangad had methode li katjme3hom 2 ou kate7tarem les conditions
        private string GenerateUserName(string FirstName, string LastName)
        {
            return FirstName.Trim().ToUpper() + "_" + LastName.Trim().ToLower();
        }
     
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "Employee");
        }
        [AllowAnonymous]
        [AcceptVerbs("Get","Post")]
        public async Task<IActionResult> CheckingExistingEmail(AccountRegisterViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Json(true);
            }  
            else
                return Json($"This Email {model.Email} is already in user");
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async  Task<IActionResult> Login(AccoutnLoginViewModel model ,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //kayakhd liya les valeurs li saisit f les imputs
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
                //ila jab lah password ou username kaynin f DB
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);

                    }
                    else
                    {
                        return RedirectToAction("index", "Employee");
                    }
                }
                ModelState.AddModelError(string.Empty, "Login Invalid !!");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditAccount(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                AppUser user = await userManager.FindByIdAsync(id);
                if(user != null)
                {
                    EditAccountViewModel model = new EditAccountViewModel()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Age = user.Age,
                        Id = user.Id,
                        
                    };
                    return View(model);
                }
            }
            return RedirectToAction("index","Employee");
        }
        [HttpPost]
        public async Task<IActionResult> EditAccount(EditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                { 
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Age = model.Age;
                    //nhachiw lpassword 3ad nsiftoh
                    var hashpw = userManager.PasswordHasher.HashPassword(user, model.Password);
                    user.PasswordHash = hashpw;

                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "Employee");
                    }
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }
        public ActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.Message = "You don't have permission to access this ressource";
            if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                ViewBag.Message += "\nPath : " + ReturnUrl;
            }
            return View(); 
        }
        [HttpGet]
        public ActionResult ListUsers()
        {
            //kanqolo lih sift jami3 users ma 3ada user li dakhl lih nta (z3ma howa owner)
            var users = userManager.Users.Where(u => u.Email != User.Identity.Name);
            return View(users);
        }
        [HttpGet]
        public async Task<ActionResult> EditUser(string id)
        {

            AppUser user = await userManager.FindByIdAsync(id);
            if (user is null)
            {
                return View("NotFound", $"User with Id = {id} Connot be found");
            }
            var userRoles = await userManager.GetRolesAsync(user);
            var userClaims = await userManager.GetClaimsAsync(user);
            AccountEditUserViewModel model = new AccountEditUserViewModel()
            {
                Age = user.Age,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Roles = userRoles,
                Claims = userClaims.Select(c => c.Value).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> EditUser(AccountEditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByIdAsync(model.Id);
                if (user is null)
                {
                    return View("NotFound", $"User with Id = {model.Id} Connot be found");
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Age = model.Age;
                user.Email = model.Email;
                IdentityResult resultat = await userManager.UpdateAsync(user);
                if (resultat.Succeeded)
                {
                    return RedirectToAction(nameof(ListUsers));
                }
                //foreach (var error in resultat.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //    //7ta hna ghan3awdo nefs lcode dyal chargement de roles de l'utilisateur
                //}
            }
            else
            {
                AppUser user = await userManager.FindByIdAsync(model.Id);
                if (user is null)
                {
                    return View("NotFound", $"User with Id = {model.Id} Connot be found");
                }
                var userRoles = await userManager.GetRolesAsync(user);
                var userClaims = await userManager.GetClaimsAsync(user);
                model.Roles = userRoles;
                model.Claims = userClaims.Select(c => c.Value).ToList();
            }
            return View(model);
            
        }
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user is null)
            {
                return View("NotFound", $"User with Id = {id} Connot be found");
            }
            
            IdentityResult resultat = await userManager.DeleteAsync(user);
            if (!resultat.Succeeded)
            {
                foreach (var error in resultat.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction(nameof(ListUsers));

        }
    }
}
