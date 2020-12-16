using System;

namespace Bank
{
    class Card
    {
        private const int StandartNumberOfDigitsInCardNumber = 20;

        private ConsoleKey Key;

        public bool TransactionCompleted { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public int[] Number { get; set; }

        public string Password { get; set; }

        public Account Account { get; set; }

        private Communication Communication;

        public Card()
        {
            Number = new int[StandartNumberOfDigitsInCardNumber];

            Account = new Account();

            Communication = new Communication();
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
            if (Key == ConsoleKey.Escape)
            {
                return;
            }

            if (Key == ConsoleKey.Enter)
            {
                Account.Money += money;
            }
        }

        public void WithdrawMoney()
        {
            double money = InputMoney();

            GetAgreement();

            if (Key == ConsoleKey.Escape)
            {
                return;
            }

            if (Key == ConsoleKey.Enter)
            {
                if (Account.Money >= money)
                {
                    Account.Money -= money;

                    TransactionCompleted = true;
                }
                else
                {
                    Communication.GetMessageAboutMoneyShortageError();

                    TransactionCompleted = false;

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

            if (Key == ConsoleKey.Escape)
            {
                return;
            }

            if (Key == ConsoleKey.Enter)
            {
                Account.Credit += money;

                Account.Money += money;
            }
        }

        public void WithdrawCredit()
        {
            GetAgreement();

            if (Key == ConsoleKey.Escape)
            {
                return;
            }

            if (Key == ConsoleKey.Enter)
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

                TransactionCompleted = true;
            }
            else
            {
                Communication.GetMessageAboutMoneyShortageError();

                TransactionCompleted = false;

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
                Communication.GetVerificationInstruction();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        Key = ConsoleKey.Enter;
                        break;
                    case ConsoleKey.Escape:
                        Key = ConsoleKey.Escape;
                        break;
                    default:
                        Key = Action.UnknownKey;
                        break;
                }

                Console.Clear();
            }
            while (Key == Action.UnknownKey);
        }
    }
}
