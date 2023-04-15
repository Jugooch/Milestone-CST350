using Microsoft.AspNetCore.Mvc;
using Milestone_CST350.Models;
using Milestone_CST350.Services;

namespace Milestone_CST350.Controllers
{
	public class LoginController : Controller
	{
        MongoUsrersDAO mongoUsrers = new MongoUsrersDAO();

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
                mongoUsrers.CreateUserAsync(new MongoUser(user.FirstName, user.LastName, user.Sex, user.Age, user.State, user.Email, user.Username, user.Password));
                return RedirectToAction("Index", "Home");
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

        public IActionResult LoginAuthenticate(MongoUser user)
        {
			SecurityService service = new SecurityService();
			if (service.IsValid(user)){

                // Find by username and password
                MongoUser mongoUser = mongoUsrers.GetByNameAndPassword(user);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1), // Set the cookie expiration date
                    HttpOnly = true // Prevent JavaScript access to the cookie
                };
                HttpContext.Response.Cookies.Append("UserId", mongoUser.ID.ToString(), cookieOptions);

                return RedirectToAction("Index", "Home");
			}
			else
			{
				return RedirectToAction("LoginFailed", "Login");
			}
        }
    }
}
