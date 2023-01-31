using ButtonGrid.Models;
using Microsoft.AspNetCore.Mvc;
using Milestone_CST350.Models;
using System.Diagnostics;

namespace Milestone_CST350.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        static List<ButtonModel> buttons = new List<ButtonModel>();
		int difficulty;
		static GridModel gridModel = new GridModel(0);

        public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult SelectDifficulty()
		{
			return View();
		}
		public IActionResult DifficultySelected(GridModel grid) 
		{ 
			gridModel = new GridModel(grid.Difficulty);
			buttons = gridModel.Buttons;
			return View("Minesweeper", gridModel);
		}

		public IActionResult Minesweeper()
		{
			return View(gridModel);
        }
        public IActionResult HandleButtonClick(string buttonNumber)
        {
            int bN = int.Parse(buttonNumber);

            buttons.ElementAt(bN).Clicked = true;
			gridModel.Buttons = buttons;

            return View("Minesweeper", gridModel);
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}