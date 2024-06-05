using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStocks.DbStuff.Repositories;
using WebStocks.Models;
using WebStocks.Services;

namespace WebStocks.Controllers
{
    public class UserController : Controller
    {
        private AuthService _authService { get; set; }
        private UserRepository _userRepository { get; set; }

        public UserController(AuthService authService, UserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public IActionResult Profile()
        {
            var viewModel = new UserIndexViewModel();
            viewModel.CurrentLocale = _authService.GetCurrentUserLocale();

            return View(viewModel);
        }

        [Authorize]
        public IActionResult SwitchLocale(string locale)
        {
            var userId = _authService.GetCurrentUserId().Value;
            _userRepository.SwitchLocal(userId, locale);

            return RedirectToAction("Profile");
        }
    }
}
