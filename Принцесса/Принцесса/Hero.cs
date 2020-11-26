using System;
using System.Collections.Generic;
using System.Text;

namespace Принцесса
{
    public class Hero
    {
        public int HillPoints { get; set; } = 10;
        public void CheckHillPoints()
        {
            Console.WriteLine($"Health amount: {HillPoints}");
        }
    }
}
