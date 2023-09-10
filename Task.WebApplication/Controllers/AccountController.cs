using Greenz.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task.Core.Dtos;
using Task.Core.Entities;
using Task.Core.Interfaces;
using Task.Services.UserService;

namespace Task.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserService _userService;
        private readonly RoleManager<Role> roleManager;

        public AccountController(RoleManager<Role> roleManager, UserManager<ApplicationUser> userManager,
       SignInManager<ApplicationUser> signInManager , IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _userService = userService;
            this.roleManager = roleManager;

        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            ViewData["roles"] = new SelectList(roleManager.Roles, "Name", "Name");
            return View();
        }


        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to IdentityUser
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    IsNew = true
                };

                // Store user data in AspNetUsers database table
                var result = await userManager.CreateAsync(user, "Asd_123456");
                IdentityResult result2 = null;
                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController
                if (result.Succeeded)
                {
                    result2 = await userManager.AddToRoleAsync(user, model.RoleName);
                    //   await signInManager.SignInAsync(user, isPersistent: false);
                    // return RedirectToAction("index", "home");

                    if (result2.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }


                    ////  await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewData["roles"] = new SelectList(roleManager.Roles, "Name", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string ReturnUrl)
        {
            await signInManager.SignOutAsync();
            return LocalRedirect(ReturnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.URL = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await signInManager.PasswordSignInAsync(
                    model.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user.IsNew)
                {
                    return RedirectToAction("ResetPassword", new { id = user.Id });
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with ID = {Id} cannot be found";
                return NotFound();
            }
            ViewBag.Id = user.Id;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with ID = {model.Id} cannot be found";
                return NotFound();
            }
            //Set new Password
            if (!String.IsNullOrEmpty(model.NewPassword))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                {
                    user.IsNew = false;
                    await _userService.UpdateUser(user);
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    ViewBag.Id = user.Id;
                    return View(model);
                }
            }
            //   await userManager.UpdateSecurityStampAsync(user);
            //    await signInManager.RefreshSignInAsync(user);
            ViewBag.Id = user.Id;
            return View();
        }
    }
}
