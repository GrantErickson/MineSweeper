using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MineSweeper.Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void CreateBoard()
        {
            var game = new GameHarness(10, 10, 10);
            Assert.AreEqual(10, game.MineCount());
        }

        [TestMethod]

        public void CreateBadBoard()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                var game = new GameHarness(10, 10, 101);
            });
        }

        [TestMethod]

        public void NumberFill1()
        {
            var game = new GameHarness(2, 2, 0);

            game.SetMine(1, 1);
            Assert.AreEqual(-1, game.GetBoard(1, 1));
            Assert.AreEqual(1, game.GetBoard(1, 2));
            Assert.AreEqual(1, game.GetBoard(2, 2));
            Assert.AreEqual(1, game.GetBoard(2, 1));
        }

        [TestMethod]
        public void NumberFill2()
        {
            var game = new GameHarness(3, 3, 0);

            game.SetMine(2, 2);
            Assert.AreEqual(1, game.GetBoard(1, 1));
            Assert.AreEqual(1, game.GetBoard(1, 2));
            Assert.AreEqual(-1, game.GetBoard(2, 2));
            Assert.AreEqual(1, game.GetBoard(2, 1));
        }

        [TestMethod]
        public void NumberFill3()
        {
            var game = new GameHarness(3, 3, 0);

            game.SetMine(2, 2);
            Assert.AreEqual(1, game.GetBoard(1, 1));
            Assert.AreEqual(1, game.GetBoard(2, 1));
            Assert.AreEqual(1, game.GetBoard(3, 1));
            Assert.AreEqual(1, game.GetBoard(1, 2));
            Assert.AreEqual(-1, game.GetBoard(2, 2));
            Assert.AreEqual(1, game.GetBoard(3, 2));
            Assert.AreEqual(1, game.GetBoard(1, 3));
            Assert.AreEqual(1, game.GetBoard(2, 3));
            Assert.AreEqual(1, game.GetBoard(3, 3));
        }

        [TestMethod]
        public void NumberFill4()
        {
            var game = new GameHarness(3, 3, 0);

            game.SetMine(2, 2);
            game.SetMine(1, 1);
            Assert.AreEqual(-1, game.GetBoard(1, 1));
            Assert.AreEqual(2, game.GetBoard(2, 1));
            Assert.AreEqual(1, game.GetBoard(3, 1));
            Assert.AreEqual(2, game.GetBoard(1, 2));
            Assert.AreEqual(-1, game.GetBoard(2, 2));
            Assert.AreEqual(1, game.GetBoard(3, 2));
            Assert.AreEqual(1, game.GetBoard(1, 3));
            Assert.AreEqual(1, game.GetBoard(2, 3));
            Assert.AreEqual(1, game.GetBoard(3, 3));
        }

        [TestMethod]

        public void Losing()
        {
            var game = new GameHarness(2, 2, 0);
            game.SetMine(1, 1);
            game.Move(1, 1);
            Assert.AreEqual(Game.GameStates.Lost, game.GameState);
        }

        [TestMethod]

        public void Winning()
        {
            var game = new GameHarness(2, 2, 0);
            game.SetMine(1, 1);
            game.SetMine(2, 2);
            game.Move(1, 2);
            game.Move(2, 1);
            Assert.AreEqual(Game.GameStates.Won, game.GameState);
        }

        [TestMethod]
        public void MarkMine()
        {
            var game = new GameHarness(2, 2, 0);
            game.SetMine(1, 1);
            game.MarkMine(1, 1);
            Assert.AreEqual(-1, game.GetPlayer(1,1));
            Assert.AreEqual(1, game.MarkedMineCount());
            game.MarkMine(1, 1);
            Assert.AreEqual(0, game.GetPlayer(1, 1));
        }

        [TestMethod]
        public void MoveOnMarkedMine()
        {
            var game = new GameHarness(2, 2, 0);
            game.SetMine(1, 1);
            game.MarkMine(1, 1);
            game.Move(1, 1);
            Assert.AreEqual(Game.GameStates.Active, game.GameState);
        }



        [TestMethod]
        public void Draw()
        {
            var game = new GameHarness(2, 2, 0);
            game.SetMine(1, 1);
            game.MarkMine(2, 1);
            game.Move(2, 2);
            var print = game.Draw();
            var lines = print.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(2, lines.Length);
            Assert.AreEqual(". * ", lines[0]);
            Assert.AreEqual(". 1 ", lines[1]);
            game.Move(1,1);
            var print2 = game.Draw();
            var lines2 = print2.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(2, lines2.Length);
            Assert.AreEqual("X * ", lines2[0]);
            Assert.AreEqual(". 1 ", lines2[1]);
        }

        [TestMethod]
        public void ExposeBoard1()
        {
            var game = new GameHarness(4, 4, 0);
            game.SetMine(1, 1);
            game.Move(4, 4);
            Assert.AreEqual(15, game.ExposedCount());
        }
        [TestMethod]
        public void ExposeBoard2()
        {
            var game = new GameHarness(4, 4, 0);
            game.SetMine(4, 3);
            game.Move(4, 4);
            Assert.AreEqual(1, game.ExposedCount());
        }

        [TestMethod]
        public void ExposeBoard3()
        {
            var game = new GameHarness(4, 4, 0);
            game.SetMine(2, 2);
            game.Move(4, 4);
            Assert.AreEqual(12, game.ExposedCount());
        }

        [TestMethod]
        public void IllegalMove()
        {
            var game = new GameHarness(1, 1, 0);
            game.Move(2, 2);
            Assert.AreEqual(0, game.ExposedCount());
        }
        [TestMethod]
        public void IllegalMineMark()
        {
            var game = new GameHarness(1, 1, 0);
            game.MarkMine(2, 2);
            Assert.AreEqual(0, game.MarkedMineCount());
        }

    }
}