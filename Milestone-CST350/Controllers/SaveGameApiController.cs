using ButtonGrid.Models;
using Microsoft.AspNetCore.Mvc;
using Milestone_CST350.Models;
using Milestone_CST350.Services;
using Minesweeper_GUI;
using System.Web.Http.Description;

namespace Milestone_CST350.Controllers
{

    [ApiController]
    [Route("Api/MinesweeperSaves")]
    public class SaveGameApiController : Controller
    {
        MinesweeperService minesweeperService = new MinesweeperService();

        /*
         * 
         * THIS SHOULD UPDATE WHEN A USER IS LOGGED IN AND STAY UNTIL LOGGED OUT/APP QUIT
         * 
         */
        UserModel LoggedInUser = new UserModel();

        /// <summary>
        /// This should be used to show how many saved games the user has. Should be used when 
        /// allowing a user to select a saved game
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<int>))]
        public List<int> Index()
        {

            /*
             * 
             * THIS SHOULD USE THE LOGGED IN USER"S ID TO FIND THEIR SAVED GAMES
             * 
             */
            return minesweeperService.GetAllBoards(LoggedInUser.ID);
        }

        /// <summary>
        /// Get API method to get the specific cells for a game. Should be used to continue a specified game by ID
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet("/{gameId}")]
        [ResponseType(typeof(Board))]
        public Board GetBoard(int gameId)
        {
            return minesweeperService.GetBoard(gameId);
        }
        
        /// <summary>
        /// Delete API method that should delete the game with the ID passed in as a param
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpDelete("/{gameId}")]
        [ResponseType(typeof(int))]
        public int DeleteBoard(int gameId)
        {
            return minesweeperService.DeleteBoard(gameId);
        }
    }
}
