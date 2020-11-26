using System;

namespace Принцесса
{  
    class Program
    {
        static void Main(string[] args)
        {
            bool end = false;
            string selection;
            do
            {
                Game game = new Game();
                game.StartGame();
                do
                {
                    Console.Clear();
                    Console.Write("Start a new game?\nWrite(Yes(Y/y) or No(N/n)):");
                    selection = Console.ReadLine();
                } while (selection != "Y" && selection != "y" && selection != "N" && selection != "n");
                switch (selection)
                {
                    case "Y":
                    case "y":
                        end = false;
                        break;
                    case "N":
                    case "n":
                        end = true;
                        break;
                }
            } while (end == false);
        }
    }
}
