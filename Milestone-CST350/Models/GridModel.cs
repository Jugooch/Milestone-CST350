using ButtonGrid.Models;

namespace Milestone_CST350.Models
{
    public class GridModel
    {
        public int Difficulty { get; set; }
        public List<ButtonModel> Buttons { get; set; }

        public GridModel() { }

        public GridModel(int difficulty) 
        {
            int gridSize = 0;
            this.Difficulty = difficulty;
            this.Buttons = new List<ButtonModel>();
            if(Difficulty == 1) { gridSize = 25; }
            else if (Difficulty == 2) { gridSize = 64; }
            else { gridSize = 100; }
            for (int i = 0; i < gridSize; i++) 
            {
                Buttons.Add(new ButtonModel(i, false, false));
            }
        }
    }
}
