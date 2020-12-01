using System;

namespace Princess
{
    public class Program
    {
        private static void Main(string[] args)
        {
            bool end = false;
            bool trueKey;
            do
            {
                Game game = new Game();
                game.StartGame();

                do
                {
                    Console.Clear();
                    Console.Write("Start a new game?\nPress Enter(Yes) or Esc(No)");

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Enter:
                            trueKey = true;
                            end = false;
                            break;
                        case ConsoleKey.Escape:
                            trueKey = true;
                            end = true;
                            break;
                        default:
                         trueKey= false;
                            break;
                    }

                } while (trueKey == false);

            } while (end == false);
        }
    }
}
