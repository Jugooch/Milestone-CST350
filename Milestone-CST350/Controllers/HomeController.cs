using ButtonGrid.Models;
using Microsoft.AspNetCore.Mvc;
using Milestone_CST350.Models;
using Minesweeper_GUI;
using System.Diagnostics;

namespace Milestone_CST350.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        static Cell[,] buttons;
		static Board board = new Board();
		static GridModel gridModel = new GridModel();

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

		public IActionResult Minesweeper(GridModel grid)
        {
            board = new Board(10, (float)grid.Difficulty);
            buttons = board.Grid;
            board.setupBombs();
            board.CalcLiveNeighbors();
            gridModel = new GridModel(board);
            return View("Minesweeper", gridModel);
        }

        public IActionResult HandleButtonClick(int bN)
		{ 

			for(int i = 0; i < 10; i++)
			{
				for(int x = 0; x< 10; x++)
				{
					if (buttons[i, x].ID == bN)
					{
                        //leftClick
                        board.leftClick(i, x);
                    }
				}
			}

			board.Grid = buttons;
			gridModel = new GridModel(board);

			if (board.checkForWin())
			{
				Console.WriteLine("You have won...");
				return View("MinesweeperWin", gridModel);
			}
			else if (board.checkForLose())
			{
				return View("MinesweeperLose", gridModel);
			}
			else {
				return PartialView("_Grid", gridModel);
			}
        }

        public IActionResult HandleButtonFlag(int bN)
        {

            for (int i = 0; i < 10; i++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (buttons[i, x].ID == bN)
                    {
                        board.rightClick(i, x);
                    }
                }
            }
			gridModel = new GridModel(board);

            return PartialView("_Grid", gridModel);
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