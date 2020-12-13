using System;

namespace Bank
{
    class Card
    {
        private string agreement;

        public bool Transaction { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public int[] Number { get; set; }

        public string Password { get; set; }


        public Account account;

        public Card()
        {
            Number = new int[20];

            account = new Account();
        }
        public virtual void ConnectCards(Account newAccount)
        {
            newAccount.Money += account.Money;
            newAccount.Credit += account.Credit;

            account = newAccount;
        }
        public virtual void AddMoney()
        {
            double money = 0.0;

            InputMoney(ref money);

            GetAgreement();
            if (agreement == "N" || agreement == "n")
            {
                return;
            }
            else
            {
                account.Money += money;
            }
        }
        public virtual void WithdrawMoney()
        {
            double money = 0.0;
            InputMoney(ref money);
            GetAgreement();
            if (agreement == "N" || agreement == "n")
            {
                return;
            }
            else
            {
                if (account.Money >= money)
                {
                    account.Money -= money;
                    Transaction = true;
                }
                else
                {
                    Console.WriteLine("You cannot withdraw more money than you have!\nBut you can take out a credit.\nPress any key to continue.");
                    Transaction = false;
                    Console.ReadKey();
                }
            }
        }
        public virtual void CheckMoney()
        {
            Console.WriteLine("\nMoney on the account: " + account.Money);
        }
        public void TransactionMoney(double money)
        {
            account.Money += money;
        }
        public virtual void ShowCredit()
        {
            Console.WriteLine("Your credit: " + account.Credit);
        }
        public virtual bool CheckCredit()
        {
            bool checkCredit;
            if (account.Credit > 0.0)
            {
                checkCredit = true;
            }
            else
            {
                checkCredit = false;
            }
            return checkCredit;
        }
        public virtual void AddCredit()
        {
            double money = 0.0;
            InputMoney(ref money);
            GetAgreement();
            if (agreement == "N" || agreement == "n")
            {
                return;
            }
            else
            {
                account.Credit += money;
                account.Money += money;
            }
        }

        public virtual void WithdrawCredit()
        {
            GetAgreement();
            if (agreement == "N" || agreement == "n")
            {
                return;
            }
            else
            {
                if (account.Credit == 0)
                {
                    Console.WriteLine("You have no credit.");
                }
                if (account.Credit >= account.Money)
                {
                    account.Credit -= account.Money;
                    account.Money = 0;
                }
                else
                {
                    account.Money -= account.Credit;
                    account.Credit = 0;
                }
            }
        }
        public virtual void AddTransactionMoney(double money)
        {
            account.Money += money;
        }
        public virtual void WithdrawTransactionMoney(double money)
        {
            if (account.Money >= money)
            {
                account.Money -= money;
                Transaction = true;
            }
            else
            {
                Console.WriteLine("You cannot transfer more money than you have!\nBut you can take out a credit.\nPress any key to continue.");
                Transaction = false;
                Console.ReadKey();
            }
        }
        public virtual double InputMoney(ref double value)
        {
            do
            {
                Console.Write("\nAmount of money: ");
            }
            while (!double.TryParse(Console.ReadLine(), out value) || value < 0);

            return value;
        }
        public virtual void GetAgreement()
        {
            do
            {
                Console.Write("Are you sure of your action? (please write yes-Y(y) or no-N(n)): ");
                agreement = Console.ReadLine();
            } while (agreement != "N" && agreement != "Y" && agreement != "n" && agreement != "y");
        }
    }
}
