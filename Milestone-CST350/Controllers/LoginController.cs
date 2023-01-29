using Microsoft.AspNetCore.Mvc;
using Milestone_CST350.Models;
using Milestone_CST350.Services;

namespace Milestone_CST350.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SignUp()
		{
			return View();
		}

        public IActionResult CreateAccount(UserModel user)
        {
            UserService service = new UserService();
            if (service.IsValid(user))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
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
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return RedirectToAction("LoginFailed", "Login");
			}
        }
    }
}
