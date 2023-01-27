using Microsoft.AspNetCore.Mvc;

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
	}
}
