using System;

namespace Princess
{
    public class Mine
    {
        Random random = new Random();

        public int[,] Damage { get; set; } = new int[10, 10];

        public void GetMine()
        {
            for (int amount = 0; amount < 10; amount++)
            {
                int row = random.Next(0, 9);
                int column = random.Next(0, 9);

                Damage[row, column] = random.Next(1, 10);
            }
        }
    }
}
