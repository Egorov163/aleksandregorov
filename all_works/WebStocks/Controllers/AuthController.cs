using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebStocks.DbStuff.Repositories;
using WebStocks.Models.Auth;
using WebStocks.Services;

namespace WebStocks.Controllers
{
    public class AuthController : Controller
    {
        private UserRepository _userRepository;


        public const string AUTH_KEY = "Smile";

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthViewModel authViewModel)
        {
            var user = _userRepository.GetUserByLoginAndPassword(authViewModel.UserName, authViewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError(nameof(authViewModel.UserName), "Wrong name or password");
                return View(authViewModel);
            }

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Login.ToString()),
                new Claim(AuthService.LOCALE_TYPE, user.PreferLocale),
                new Claim("email", user.Email??"")
            };
            var identity = new ClaimsIdentity(claims, AUTH_KEY);
            var principal = new ClaimsPrincipal(identity);
            HttpContext
                .SignInAsync(AUTH_KEY, principal)
                .Wait();
            return RedirectToAction("Home", "StocksPortfolio");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Home", "StocksPortfolio");
        }
    }
}
