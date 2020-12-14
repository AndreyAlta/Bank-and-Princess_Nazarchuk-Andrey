using System;

namespace Bank
{
    class Card
    {
        private ConsoleKey key;

        public bool Transaction { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public int[] Number { get; set; }

        public string Password { get; set; }

        public Account Account { get; set; }

        private Communication communication;

        public Card()
        {
            Number = new int[20];

            Account = new Account();

            communication = new Communication();
        }
        public void ConnectCards(Account newAccount)
        {
            newAccount.Money += Account.Money;

            newAccount.Credit += Account.Credit;

            Account = newAccount;
        }
        public void AddMoney()
        {
            double money = InputMoney();

            GetAgreement();
            if (key == ConsoleKey.Escape)
            {
                return;
            }

            if (key == ConsoleKey.Enter)
            {
                Account.Money += money;
            }
        }
        public void WithdrawMoney()
        {
            double money = InputMoney();

            GetAgreement();

            if (key == ConsoleKey.Escape)
            {
                return;
            }

            if (key == ConsoleKey.Enter)
            {
                if (Account.Money >= money)
                {
                    Account.Money -= money;

                    Transaction = true;
                }
                else
                {
                    communication.GetMessageAboutMoneyShortageError();

                    Transaction = false;

                    Console.ReadKey();
                }
            }
        }

        public void CheckMoney()
        {
            Console.WriteLine("\nMoney on the account: " + Account.Money);
        }

        public void TransactionMoney(double money)
        {
            Account.Money += money;
        }

        public void ShowCredit()
        {
            Console.WriteLine("Your credit: " + Account.Credit);
        }

        public bool CheckCredit()
        {
            bool checkCredit;

            if (Account.Credit > 0.0)
            {
                checkCredit = true;
            }
            else
            {
                checkCredit = false;
            }

            return checkCredit;
        }

        public void AddCredit()
        {
            double money = InputMoney();

            GetAgreement();

            if (key == ConsoleKey.Escape)
            {
                return;
            }

            if (key == ConsoleKey.Enter)
            {
                Account.Credit += money;

                Account.Money += money;
            }
        }

        public void WithdrawCredit()
        {
            GetAgreement();

            if (key == ConsoleKey.Escape)
            {
                return;
            }

            if (key == ConsoleKey.Enter)
            {
                if (Account.Credit == 0)
                {
                    Console.WriteLine("You have no credit.");
                }

                if (Account.Credit >= Account.Money)
                {
                    Account.Credit -= Account.Money;
                    Account.Money = 0;
                }
                else
                {
                    Account.Money -= Account.Credit;
                    Account.Credit = 0;
                }
            }
        }

        public void AddTransactionMoney(double money)
        {
            Account.Money += money;
        }

        public void WithdrawTransactionMoney(double money)
        {
            if (Account.Money >= money)
            {
                Account.Money -= money;

                Transaction = true;
            }
            else
            {
                communication.GetMessageAboutMoneyShortageError();

                Transaction = false;

                Console.ReadKey();
            }
        }

        public double InputMoney()
        {
            double value;

            do
            {
                Console.Write("\nAmount of money: ");
            }
            while (!double.TryParse(Console.ReadLine(), out value) || value < 0);

            return value;
        }

        public void GetAgreement()
        {
            do
            {
                communication.GetVerificationInstruction();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        key = ConsoleKey.Enter;
                        break;
                    case ConsoleKey.Escape:
                        key = ConsoleKey.Escape;
                        break;
                    default:
                        key = Action.UnknownKey;
                        break;
                }

                Console.Clear();
            }
            while (key == Action.UnknownKey);
        }
    }
}
