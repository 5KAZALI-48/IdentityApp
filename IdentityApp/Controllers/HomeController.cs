using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IdentityApp.Extentions;
using IdentityApp.Services;

namespace IdentityApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        //public string UserId { get; private set; }

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _UserManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home");
            var hasUser = await _UserManager.FindByEmailAsync(model.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "Email or Password is wrong");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl);
            }

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(
                [
                    $"Your Account is locked, please try 3 minutes later."
                ]);
                return View();

            }
            ModelState.AddModelErrorList(
            [
                    "Email or password is wrong"
            ]);
            ModelState.AddModelErrorList([
                    $"Email or password is wrong",$"Failed= {await _UserManager.GetAccessFailedCountAsync(hasUser)}" ]);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identityResult = await _UserManager.CreateAsync(new()
            {
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                Email = request.Email

            }, request.ConfirmPassword);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Registration completed Successfully!";
                return RedirectToAction(nameof(HomeController.SignUp));
            }
            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());



            return View();
        }



        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            var hasUser = await _UserManager.FindByEmailAsync(request.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "There is no user associated with this e-mail account");
                return View();
            }

            string passwordResetToken = await _UserManager.GeneratePasswordResetTokenAsync(hasUser);
            var passwordResetLink = Url.Action("ResetPassword", "Home", new
            {
                userId = hasUser.Id,
                Token = passwordResetToken
            }, HttpContext.Request.Scheme);


            //link
            //Link will be like : https://localhost:7131?userId=12213&token=adasdsadsaksadjak
            /*ToDo: Email Service*/
            await _emailService.SendPasswordResetEmail(passwordResetLink, hasUser.Email);

            TempData["SuccessMessage"] = "Password Reset link Set to your E-mail";
            return RedirectToAction(nameof(ForgetPassword));
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            
            
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordViewModel resetRequest)
        {
            var userId = TempData["userId"];
            var token = TempData["token"];

            var hasUser = await _UserManager.FindByIdAsync(userId.ToString());

            if (userId==null || token==null)
            {
                throw new Exception("An Error Occurred!");
            }

            IdentityResult result = await _UserManager.ResetPasswordAsync(hasUser, token.ToString(), resetRequest.Password);

            if(result.Succeeded)
            {
                TempData["SuccessMessage"] = "New Password is Created!";
            }
            else
            {
                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
                return View();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
