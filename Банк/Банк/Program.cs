using System;
using static System.Console;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            Action action = new Action();
            do
            {
                WriteLine("(Each card is linked to a separate account, if you want to link two cards, then each in the 3rd section)");
                Write("1-Create debit card.\n2-Create credit card.\n3-Operations with your card.\nChoose and write: ");
                string selection;
                do
                {
                    Write("Hello,choose and write(1,2,3): ");
                    selection = ReadLine();
                } while (selection != "1" && selection != "2" && selection != "3");
                Clear();
                switch (selection)
                {
                    case "1":
                        action.CreateDebitCard();
                        break;
                    case "2":
                        action.CreateCreditCard();
                        break;
                    case "3":
                        action.GetInfo();
                        break;
                }
                Clear();
            } while (true);
        }
    }
}
