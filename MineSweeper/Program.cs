// See https://aka.ms/new-console-template for more information
using MineSweeper;

Console.WriteLine("Welcome to Mine Sweeper!");

var game = new Game(20, 10, 20);


do
{
    game.Draw();
    Console.WriteLine("Enter move x,y or *x,y to mark a mine");
    var input = Console.ReadLine();
    if (input != null)
    {
        string raw = input.Replace("*", "");
        var chars = raw.Split(',', StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);
        if (chars.Length == 2)
        {
            int x, y;
            if (int.TryParse(chars[0], out x) && int.TryParse(chars[1], out y))
            {
                if (input.StartsWith("*"))
                {
                    game.MarkMine(x, y);
                }
                else
                {
                    game.Move(x, y);
                }
            }
        }
    } 
    Console.WriteLine();
} while (game.GameState == Game.GameStates.Active);


game.Draw();
if (game.GameState == Game.GameStates.Won) Console.WriteLine("You Win!");
if (game.GameState == Game.GameStates.Lost) Console.WriteLine("Sorry, you found a mine");


