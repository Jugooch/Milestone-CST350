using Minesweeper_GUI;
using System.Data.SqlClient;

namespace Milestone_CST350.Services
{

    //find the cells from the specific game the user wants to load
    public class MinesweeperDAO
    {
        string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Milestone-350;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //This method gets all the logged in user's saved games
        public List<int> GetAllBoards(int userId)
        {
            List<int> gameIds = new List<int>();

            //Get the saved games for the user that is logged in
            string sql = "SELECT * FROM dbo.Minesweeper WHERE UserId=@userId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                sqlCommand.Parameters.AddWithValue("@userId", userId);
                try
                {
                    conn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    //add these game's ids into a list
                    while (reader.Read())
                    {
                        gameIds.Add((int)reader[0]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return gameIds;
        }

        public List<Cell> GetBoard(int gameId)
        {
            List<Cell> cells = new List<Cell>();

            string sql = "SELECT * FROM dbo.BoardCells WHERE GameId=@gameId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                try
                {
                    sqlCommand.Parameters.AddWithValue("@gameId", gameId);
                    conn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Cell cell = new Cell((int)reader[0], (int)reader[3], (int)reader[2]);
                        cell.LiveNeighbors = (int)reader[4];

                        //Check Values from database. Ints of 0/1 are a psuedo way of doing a boolean value in sql
                        //1s are true, 0s are false
                        if ((int)reader[5] == 1)
                        {
                            cell.IsVisited = true;
                        }
                        if ((int)reader[6] == 1)
                        {
                            cell.IsFlagged = true;
                        }
                        if ((int)reader[7] == 1)
                        {
                            cell.IsLive = true;
                        }

                        cells.Add(cell);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return cells;
        }

        public int DeleteBoard(int gameId)
        {
            int result = 0;

            string sql = "DELETE FROM dbo.BoardCells WHERE GameId=@gameId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                try
                {
                    sqlCommand.Parameters.AddWithValue("@gameId", gameId);
                    conn.Open();
                    result = sqlCommand.ExecuteNonQuery();

                    if(result == 0)
                    {
                        Console.WriteLine("Delete Failed... Please Try again");
                    }
           
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return result;
        }
    }
}
