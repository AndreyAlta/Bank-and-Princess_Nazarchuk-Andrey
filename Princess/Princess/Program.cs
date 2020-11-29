using System;

namespace Princess
{
    class Program
    {
        private static void Main(string[] args)
        {
            bool end = false;
            bool input = true;
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
                            input = true;
                            end = false;
                            break;
                        case ConsoleKey.Escape:
                            input = true;
                            end = true;
                            break;
                        default:
                            input = false;
                            break;
                    }
                } while (input == false);

            } while (end == false);
        }
    }
}
