using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Tests
{
    internal class GameHarness : Game
    {
        public GameHarness(int width, int height, int mines) : base(width, height, mines)
        { }

        public int MineCount()
        {
            int count = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Board[x, y] == -1) count++;
                }
            }
            return count;
        }

        public int ExposedCount()
        {
            int count = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Player[x, y] == 1) count++;
                }
            }
            return count;
        }

        public int MarkedMineCount()
        {
            int count = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Player[x, y] == -1) count++;
                }
            }
            return count;
        }

        public void SetMine(int x, int y)
        {
            Board[x - 1, y - 1] = -1;
            Mines++;
            CalculateNumbers();
        }

        public int GetBoard(int x, int y)
        {
            return Board[x - 1, y - 1];
        }

        public int GetPlayer(int x, int y)
        {
            return Player[x - 1, y - 1];
        }
    }
}
