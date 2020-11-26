using System;

namespace Принцесса
{
    public class Field
    {
        private string move;
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
            int counter = 0;
            int column = 0;
            do
            {
                do
                {
                    Console.Clear();
                    GetField();
                    hero.CheckHillPoints();
                    Console.Write("Write(A-to go left, W-to go up, D-to go Right, S-to go down): ");
                    move = Console.ReadLine();
                } while (move != "W" && move != "D" && move != "S" && move != "A" && move != "w" && move != "d" && move != "s" && move != "a");
                cell[row, column] = "ⓞ";
                switch (move)
                {
                    case "W":
                    case "w":
                        if (row > 0)
                        {
                            row -= 1;
                        }
                        break;
                    case "D":
                    case "d":
                        if (column < 9)
                        {
                            column += 1;
                        }
                        break;
                    case "S":
                    case "s":
                        if (row < 9)
                        {
                            row += 1;
                        }
                        break;
                    case "A":
                    case "a":
                        if (column > 0)
                        {
                            column -= 1;
                        }
                        break;
                }
                switch (true)
                {
                    case true when cell[row, column] == "ⓞ":
                        cell[row, column] = "H";
                        break;
                    case true when cell[row, column] == "P":
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
                    Console.Clear();
                    counter++;
                    GetField();
                    hero.CheckHillPoints();
                    Console.WriteLine($"You were blown up by a mine {counter}-th time!\nMine damage-{mine.Damage[row, column]}\nPress any key.");
                    mine.Damage[row, column] = 0;
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
            }
            else
            {
                Console.WriteLine("You spent all your health!\nGame over.\nPress any key.");
            }
            Console.ReadKey();
        }
    }
}
