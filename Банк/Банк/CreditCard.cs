using System;

namespace Bank
{
    class CreditCard
    {
        private string Agreement { get; set; }
        public bool transaction;
        public string Surname { get; set; }
        public string Name { get; set; }
        public int[] number = new int[20];
        public int password;
        public Account account = new Account();
        public void ConnectCards(Account newAccount)
        {
            newAccount.money += account.money;
            newAccount.credit += account.money;
            account = newAccount;
        }
        public void AddMoney()
        {
            double money = 0.0;
            InputMoney(ref money);
            GetAgreement();
            if (Agreement == "N" || Agreement == "n")
            {
                return;
            }
            else
            {
                account.money += money;
            }
        }
        public void WithdrawMoney()
        {
            double money = 0.0;
            InputMoney(ref money);
            GetAgreement();
            if (Agreement == "N" || Agreement == "n")
            {
                return;
            }
            else
            {
                if (account.money >= money)
                {
                    account.money -= money;
                    transaction = true;
                }
                else
                {
                    Console.WriteLine("You cannot withdraw more money than you have!\nBut you can take out a credit.\nPress any key to continue.");
                    transaction = false;
                    Console.ReadKey();
                }
            }
        }
        public void CheckMoney()
        {
            Console.WriteLine("\nMoney on the account: " + account.money);
        }
        public void ShowCredit()
        {
            Console.WriteLine("Your credit: " + account.credit);
        }
        public bool CheckCredit()
        {
            bool checkCredit;
            if (account.credit > 0.0)
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
            double money = 0.0;
            InputMoney(ref money);
            GetAgreement();
            if (Agreement == "N" || Agreement == "n")
            {
                return;
            }
            else
            {
                account.credit += money;
                account.money += money;
            }
        }
        public void WithdrawCredit()
        {
            GetAgreement();
            if (Agreement == "N" || Agreement == "n")
            {
                return;
            }
            else
            {
                if (account.credit == 0)
                {
                    Console.WriteLine("You have no credit.");
                }
                if (account.credit >= account.money)
                {
                    account.credit -= account.money;
                    account.money = 0;
                }
                else
                {
                    account.money -= account.credit;
                    account.credit = 0;
                }
            }
        }
        public void AddTransactionMoney(double money)
        {
            account.money += money;
        }
        public void WithdrawTransactionMoney(double money)
        {
            if (account.money >= money)
            {
                account.money -= money;
                transaction = true;
            }
            else
            {
                Console.WriteLine("You cannot transfer more money than you have!\nBut you can take out a credit.\nPress any key to continue.");
                transaction = false;
                Console.ReadKey();
            }
        }
        public double InputMoney(ref double value)
        {
            Console.Write("Amount of money: ");
            while (!double.TryParse(Console.ReadLine(), out value) || value < 0)
            {
                Console.Write("Amount of money: ");
            }
            return value;
        }
        private void GetAgreement()
        {
            do
            {
                Console.Write("Are you sure of your action? (please write yes-Y(y) or no-N(n)): ");
                Agreement = Console.ReadLine();
            } while (Agreement != "N" && Agreement != "Y" && Agreement != "n" && Agreement != "y");
        }
    }
}
