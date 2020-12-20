using System;

namespace Bank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Action action = new Action();

            Communication communication = new Communication();

            do
            {
                communication.GetRemark();
                communication.GetInstruction();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.NumPad1:
                        action.CreateDebitCard();
                        break;
                    case ConsoleKey.NumPad2:
                        action.CreateCreditCard();
                        break;
                    case ConsoleKey.NumPad3:
                        action.GetInfo();
                        break;
                }

                Console.Clear();
            }
            while (true);
        }
    }
}
