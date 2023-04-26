using Activity2_2.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using Milestone_CST350.Models;
using Milestone_CST350.Services;
using ILogger = Activity2_2.Services.Utilities.ILogger;

namespace Milestone_CST350.Controllers
{
	public class LoginController : Controller
	{
        MongoUsrersDAO mongoUsrers = new MongoUsrersDAO();
        ILogger _logger;

        public LoginController(ILogger logger)
        {
            _logger = logger;
        }

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
            _logger.GetInstance().Info("Entering Process Login:");
            _logger.GetInstance().Info("Parameter: " + user.Username.ToString());

            SecurityService service = new SecurityService();
			if (service.IsValid(user))
            {
                _logger.GetInstance().Info("Login Success");
                _logger.GetInstance().Info("Username:" + user.Username + ", Password: " + user.Password);
                _logger.GetInstance().Info("Exiting Process Login");

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
                _logger.GetInstance().Info("Login failure");
                _logger.GetInstance().Info("Username:" + user.Username + ", Password: " + user.Password);
                _logger.GetInstance().Info("Exiting Process Login");

                return RedirectToAction("LoginFailed", "Login");
			}
        }
    }
}
