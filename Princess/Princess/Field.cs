using System;

namespace Princess
{
    public class Field
    {
        private string[,] cell;
        private const int numberOfColumns = 10;
        private const int numberOfRows = 10;
        private const string cellIcon = "ⓞ";
        private const string heroIcon = "H";
        private const string princessIcon = "P";
        private const string heroAndPrincessIcon = heroIcon + princessIcon;

        public Field()
        {
            cell = new string[numberOfRows, numberOfColumns];

            for (int row = 0; row < numberOfRows; row++)
            {
                for (int column = 0; column < numberOfColumns; column++)
                {
                    cell[row, column] = cellIcon;
                }
            }

            cell[0, 0] = heroIcon;
            cell[numberOfRows - 1, numberOfColumns - 1] = princessIcon;
        }

        public void SetField()
        {
            Console.WriteLine("Control the arrows.");

            for (int row = 0; row < numberOfRows; row++, Console.WriteLine())
            {
                for (int column = 0; column < numberOfColumns; column++)
                {
                    Console.Write($"{cell[row, column]} ");
                }
            }
        }

        public void BeginMovement()
        {
            Mine mine = new Mine();
            Hero hero = new Hero();

            mine.SetMine();

            int row = 0;
            int column = 0;

            do
            {
                Console.Clear();
                SetField();
                hero.CheckHillPoints();

                cell[row, column] = cellIcon;

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (row > 0)
                        {
                            row -= 1;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (column < numberOfColumns - 1)
                        {
                            column += 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (row < numberOfRows - 1)
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
                    case cellIcon:
                        cell[row, column] = heroIcon;
                        break;
                    case princessIcon:
                        cell[row, column] = heroAndPrincessIcon;
                        break;
                }

                if (hero.HillPoints <= mine.Damage[row, column])
                {
                    hero.HillPoints = 0;
                }
                else
                {
                    hero.HillPoints -= mine.Damage[row, column];
                }

                if (mine.Damage[row, column] > 0)
                {
                    Console.Clear();
                    SetField();
                    hero.CheckHillPoints();

                    Console.WriteLine($"Mine damage-{mine.Damage[row, column]}\nPress any key.");
                    mine.Damage[row, column] = 0;
                    Console.ReadKey();
                }
            }
            while (cell[numberOfRows - 1, numberOfRows - 1] == "P" && hero.HillPoints > 0);

            Console.Clear();

            if (hero.HillPoints > 0)
            {
                cell[numberOfRows - 1, numberOfRows - 1] = heroAndPrincessIcon;
                SetField();
                hero.CheckHillPoints();

                Console.WriteLine("You have completed the game!\nPress any key.");
                Console.ReadKey();
            }
        }
    }
}
