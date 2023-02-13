using ButtonGrid.Models;
using Minesweeper_GUI;

namespace Milestone_CST350.Models
{
    public class GridModel
    {
        // The cell class represents one square onthe board. Grid is a 2d array of cells
        public Cell[,] Grid { get; set; }

        // the board is square. Size is the both the length and width of the board
        public int Size { get; set; }

        // difficulty is a percent value.  0.05 difficulty means that 5% of the squares will contain a bomb.
        public float Difficulty { get; set; }

        // total clicks in the game
        public int clicks;

        // starting time for the game. Used to determine score
        public DateTime startTime;

        public DateTime endTime;

        // score - calcualted at the end of the game
        public int score;

        // Create a board game with size and difficulty
        public GridModel()
        {

        }
        public GridModel(Board board)
        {
            Grid = board.Grid;
            Size = board.Size;
            Difficulty = board.Difficulty;
            clicks = board.clicks;
            startTime = board.startTime;
            endTime = board.endTime;
            score = board.score;
        }
    }
}
