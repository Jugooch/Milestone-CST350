using Minesweeper_GUI;

namespace Milestone_CST350.Services
{
    public class MinesweeperService
    {
        MinesweeperDAO minesweeperDAO = new MinesweeperDAO();

        public List<int> GetAllBoards(int gameId)
        {
           return minesweeperDAO.GetAllBoards(gameId);
        }

        public Board GetBoard(int gameId) 
        {
            List<Cell> cells = minesweeperDAO.GetBoard(gameId);
            Board board = new Board();
            int row = 0;
            for(int i = 0; i < 100; i++)
            {
                if(i%10  == 0)
                {
                    row++;
                }
                board.Grid[row, i] = cells.ElementAt(i);
            }
            return board;
        }

        public int DeleteBoard(int gameId)
        {
            return minesweeperDAO.DeleteBoard(gameId);
        }
    }
}
