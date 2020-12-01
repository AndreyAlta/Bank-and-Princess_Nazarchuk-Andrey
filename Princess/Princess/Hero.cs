using System;

namespace Princess
{
    public class Hero
    {
        public int HillPoints { get; set; }

        public Hero()
        {
            HillPoints = 10;
        }

        public void CheckHillPoints()
        {
            Console.WriteLine($"Health amount: {HillPoints}");
        }
    }
}
