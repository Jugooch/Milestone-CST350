using Microsoft.AspNetCore.Mvc;
using Milestone_CST350.Models;
using System.Diagnostics;
using Minesweeper_GUI;
using Milestone_CST350.Services;

namespace Milestone_CST350.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        static Cell[,] buttons;
		static Board board = new Board();
		static GridModel gridModel = new GridModel();

        MongoUsrersDAO MongoUsrersDAO = new MongoUsrersDAO();

        public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult PlayGame()
		{
			return View();
		}

		public IActionResult SelectDifficulty()
		{
			return View();
		}

        public IActionResult LoadGame()
        {
            string userIdCookie = HttpContext.Request.Cookies["UserId"];

            if (!string.IsNullOrEmpty(userIdCookie) && int.TryParse(userIdCookie, out int userId))
            {
                // 'userId' variable now contains the user's ID from the cookie
            }

            MongoUser user = MongoUsrersDAO.GetUserByIdAsync(userIdCookie);
            return View(user);
        }

        public IActionResult PlayLoadedGame(int id)
        {
            string userIdCookie = HttpContext.Request.Cookies["UserId"];

            if (!string.IsNullOrEmpty(userIdCookie) && int.TryParse(userIdCookie, out int userId))
            {
                // 'userId' variable now contains the user's ID from the cookie
            }

            MongoUser user = MongoUsrersDAO.GetUserByIdAsync(userIdCookie);

            return View("Minesweeper", user.Boards[id]);
        }

        public IActionResult DeleteLoadedGame(int gameId)
        {
            string userIdCookie = HttpContext.Request.Cookies["UserId"];

            if (!string.IsNullOrEmpty(userIdCookie) && int.TryParse(userIdCookie, out int userId))
            {
                // 'userId' variable now contains the user's ID from the cookie
            }

            MongoUser user = MongoUsrersDAO.GetUserByIdAsync(userIdCookie);
            user.Boards.RemoveAt(gameId);
            MongoUsrersDAO.UpdateUserAsync(userIdCookie, user);
            return View("LoadGame", user);
        }

        public IActionResult Minesweeper(GridModel grid)
        {
            string userIdCookie = HttpContext.Request.Cookies["UserId"];

            if (!string.IsNullOrEmpty(userIdCookie) && int.TryParse(userIdCookie, out int userId))
            {
                // 'userId' variable now contains the user's ID from the cookie
            }

            MongoUser user = MongoUsrersDAO.GetUserByIdAsync(userIdCookie);

            board = new Board(10, (float)grid.Difficulty);
            buttons = board.Grid;
            board.setupBombs();
            board.CalcLiveNeighbors();
            gridModel = new GridModel(board);
            gridModel.Id = user.Boards.Count;
            gridModel.Name = "Board " + user.Boards.Count;
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

            board.Grid = buttons;
            gridModel = new GridModel(board);

            return PartialView("_Grid", gridModel);
        }

		public IActionResult SaveGameBoard()
		{
            string userIdCookie = HttpContext.Request.Cookies["UserId"];

            if (!string.IsNullOrEmpty(userIdCookie) && int.TryParse(userIdCookie, out int userId))
            {
                // 'userId' variable now contains the user's ID from the cookie
            }

            MongoUser user = MongoUsrersDAO.GetUserByIdAsync(userIdCookie);
            // if there is a grid Id then replace at that index

            gridModel.Id = user.Boards.Count;
            gridModel.Name = "Board " + user.Boards.Count;


            user.Boards.Add(gridModel);
			MongoUsrersDAO.UpdateUserAsync(userIdCookie, user);
			return View();
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