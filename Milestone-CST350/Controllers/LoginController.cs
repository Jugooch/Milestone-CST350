using Microsoft.AspNetCore.Mvc;
using Milestone_CST350.Models;
using Milestone_CST350.Services;

namespace Milestone_CST350.Controllers
{
	public class LoginController : Controller
	{
		MinesweeperService minesweeperService = new MinesweeperService();
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SignUp()
		{
			return View();
		}

		public IActionResult SignupFailed()
		{
			return View();
		}

        public IActionResult CreateAccount(UserModel user)
        {
            UserService service = new UserService();
            if (service.IsValid(user))
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Home", action = "Index", userId = user.ID }));
            }
            else
            {
                return RedirectToAction("SignupFailed", "Login");
            }
        }

		public IActionResult LoginFailed()
		{
			return View();
		}

        public IActionResult LoginAuthenticate(UserModel user)
        {
			SecurityService service = new SecurityService();
			if (service.IsValid(user)){
				return RedirectToAction("Index", new RouteValueDictionary( new { controller = "Home", action = "Index", userId = user.ID }));
			}
			else
			{
				return RedirectToAction("LoginFailed", "Login");
			}
        }
    }
}
