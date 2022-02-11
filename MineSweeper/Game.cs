using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Game
    {
        // Board
        // -1 = mine
        // 1-8 numer of adjacent mines

        // Player
        // -1 Marked as a mine
        // 0 Unexplored
        // 1 Explored
        // 2 Found a mine

        public int Width { get; }
        public int Height { get; }
        public int Mines { get; protected set; }

        protected int[,] Board { get; }
        protected int[,] Player { get; }
        public enum GameStates
        {
            Active,
            Won,
            Lost
        }

        public GameStates GameState { get; internal set; } = GameStates.Active;

        // List of directions
        private List<(int x, int y)> Directions = new()
        {
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0, -1),
            (0, 1),
            (1, -1),
            (1, 0),
            (1, 1),
        };

        public Game(int width, int height, int mines)
        {
            if (width * height - mines < 0) throw new ArgumentException("There are more mines than spaces.");
            Width = width;
            Height = height;
            Mines = mines;

            Board = new int[width, height];
            Player = new int[width, height];
            SetMines();
            CalculateNumbers();
        }

        protected void SetMines()
        {
            var minesToPlace = Mines;
            var random = new Random();
            while (minesToPlace > 0)
            {
                long x = random.NextInt64(Width);
                long y = random.NextInt64(Height);
                if (Board[x, y] == 0)
                {
                    Board[x, y] = -1;
                    minesToPlace--;
                }
            } ;
        }

        protected void CalculateNumbers()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Board[x, y] == -1)
                    {
                        foreach (var direction in Directions)
                        {
                            int newX = x + direction.x;
                            int newY = y + direction.y;
                            if (newX >= 0 && newX < Width && y + newY >= 0 && newY < Height)
                            {
                                if (Board[newX, newY] != -1) Board[newX, newY]++;
                            }
                        }
                    }
                }
            }
        }

        public string Draw()
        {
            var result = new StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Player[x, y] == -1) result.Append("* ");
                    if (Player[x, y] == 0) result.Append(". ");
                    if (Player[x, y] == 1) result.Append($"{Board[x, y]} ");
                    if (Player[x, y] == 2) result.Append("X ");
                }
                result.AppendLine();
            }
            return result.ToString();
        }

        public void Move(int xPlay, int yPlay)
        {
            if (xPlay > 0 && yPlay > 0 && xPlay <= Width && yPlay <= Height)
            {
                int x = xPlay - 1;
                int y = yPlay - 1;
                if (Player[x, y] == -1) return; // Marked already

                // If a mine, then game over.
                if (Board[x, y] == -1)
                {
                    Player[x, y] = 2;
                    GameState = GameStates.Lost;
                    return;
                }

                Player[x, y] = 1;
                if (Board[x, y] == 0)
                {
                    ExposeBoard(x, y);
                }
                TestForWin();
            }
        }

        public void MarkMine(int xPlay, int yPlay)
        {
            if (xPlay > 0 && yPlay > 0 && xPlay <= Width && yPlay <= Height)
            {
                int x = xPlay - 1;
                int y = yPlay - 1;
                if (Player[x, y] == 0)
                {
                    Player[x, y] = -1;
                }
                else
                {
                    Player[x, y] = 0;
                }
            }
        }

        private void TestForWin()
        {
            int exploredCount = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Player[x, y] == 1) exploredCount++;
                }
            }
            if (exploredCount == Width * Height - Mines)
            {
                GameState = GameStates.Won;
            }
        }

        /// <summary>
        /// Call this with when a cell is 0 mines nearby to expose adjacent cells recursively.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void ExposeBoard(int x, int y)
        {
            foreach (var direction in Directions)
            {
                int newX = x + direction.x;
                int newY = y + direction.y;
                if (newX >= 0 && newX < Width && y + newY >= 0 && newY < Height)
                {
                    if (Player[newX, newY] == 0) // Unexplored
                    {
                        Player[newX, newY] = 1; // Mark as Explored
                        if (Board[newX, newY] == 0)
                        {
                            ExposeBoard(newX, newY);
                        }
                    }
                }
            }
        }


    }
}
