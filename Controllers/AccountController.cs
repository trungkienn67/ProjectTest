using JWL.Models;
using JWL.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JWL.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUserModel> _userManager;
		private SignInManager<AppUserModel> _signManager;

        public AccountController(SignInManager<AppUserModel> signInManage, UserManager<AppUserModel> userManage)
		{
			_signManager = signInManage;
			_userManager = userManage;	
		}
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
				if (result.Succeeded)
				{
					return Redirect(loginVM.ReturnUrl ?? "/");
				}
				ModelState.AddModelError("", "Tên UserName hoặc Password chưa chính xác.");
			}
			return View(loginVM);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(UserModel user)
		{
			if (ModelState.IsValid)
			{
				AppUserModel newUser = new AppUserModel { UserName = user.UserName, Email = user.Email};
				IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
				{
					TempData["success"] = "Tạo Account thành công.";
					return Redirect("/account/login");
				}
				foreach(IdentityError error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
            }
			return View(user);
		}
		public async Task<IActionResult> Logout(string returnUrl = "/")
		{
			await _signManager.SignOutAsync();
			return Redirect(returnUrl);
		}
	}
}
