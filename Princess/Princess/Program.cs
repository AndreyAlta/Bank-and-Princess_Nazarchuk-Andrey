using System;

namespace Princess
{
    public class Program
    {
        private const string gameContinuationMessage = "Start a new game?\nPress Enter(Yes) or Esc(No)";
        private static void Main(string[] args)
        {
            bool isGameEnd = false;
            bool isEnterOrEscape = false;
            do
            {
                Game game = new Game();
                game.StartGame();

                do
                {
                    Console.Clear();
                    Console.Write(gameContinuationMessage);

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Enter:
                            isEnterOrEscape = true;
                            isGameEnd = false;
                            break;

                        case ConsoleKey.Escape:
                            isEnterOrEscape = true;
                            isGameEnd = true;
                            break;

                        default:
                            isEnterOrEscape = false;
                            break;
                    }
                }
                while (!isEnterOrEscape);
            }
            while (!isGameEnd);
        }
    }
}
