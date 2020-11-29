using System;

namespace Princess
{
    public class Field
    {
        private string[,] cell = new string[10, 10];

        public Field()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    cell[row, column] = "ⓞ";
                }
            }
            cell[0, 0] = "H";
            cell[9, 9] = "P";
        }

        public void GetField()
        {
            Console.WriteLine("Control the arrows.");

            for (int row = 0; row < 10; row++, Console.WriteLine())
            {
                for (int column = 0; column < 10; column++)
                {
                    Console.Write($"{cell[row, column]} ");
                }
            }
        }

        public void GetMovement()
        {
            Mine mine = new Mine();
            Hero hero = new Hero();

            mine.GetMine();

            int row = 0;
            int column = 0;

            do
            {
                Console.Clear();
                GetField();
                hero.CheckHillPoints();

                cell[row, column] = "ⓞ";

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (row > 0)
                        {
                            row -= 1;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (column < 9)
                        {
                            column += 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (row < 9)
                        {
                            row += 1;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (column > 0)
                        {
                            column -= 1;
                        }
                        break;
                }

                switch (cell[row, column])
                {
                    case "ⓞ":
                        cell[row, column] = "H";
                        break;
                    case "P":
                        cell[row, column] = "PH";
                        break;
                }

                if (hero.HillPoints <= mine.Damage[row, column])
                {
                    hero.HillPoints = 0;
                }

                if (hero.HillPoints >= mine.Damage[row, column])
                {
                    hero.HillPoints -= mine.Damage[row, column];
                }

                if (mine.Damage[row, column] > 0)
                {
                    mine.Damage[row, column] = 0;

                    Console.Clear();
                    GetField();
                    hero.CheckHillPoints();

                    Console.WriteLine($"Mine damage-{mine.Damage[row, column]}\nPress any key.");
                    Console.ReadKey();
                }

            } while (cell[9, 9] == "P" && hero.HillPoints > 0);

            Console.Clear();

            if (hero.HillPoints > 0)
            {
                cell[9, 9] = "PH";
                GetField();
                hero.CheckHillPoints();
                Console.WriteLine("You have completed the game!\nPress any key.");
                Console.ReadKey();
            }
        }
    }
}
