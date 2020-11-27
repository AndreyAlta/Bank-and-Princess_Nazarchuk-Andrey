using System;

namespace Bank
{
    class DebitCard
    {
        private string agreement;
        public bool Transaction { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int[] Number { get; set; } = new int[20];
        public int Password { get; set; }
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
            if (agreement == "N" || agreement == "n")
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
            InputMoney(ref money); GetAgreement();
            if (agreement == "N" || agreement == "n")
            {
                return;
            }
            else
            {
                if (account.money >= money)
                {
                    account.money -= money;
                    Transaction = true;
                }
                else
                {
                    Console.WriteLine("You cannot withdraw more money than you have! Press any key to continue.");
                    Transaction = false;
                    Console.ReadKey();
                }
            }
        }
        public void CheckMoney()
        {
            Console.WriteLine("\nMoney on the account: " + account.money);
        }
        public void TransactionMoney(double money)
        {
            account.money += money;
        }
        public void WithdrawTransactionMoney(double money)
        {
            if (account.money >= money)
            {
                account.money -= money;
                Transaction = true;
            }
            else
            {
                Console.WriteLine("You cannot transfer more money than you have!\nPress any key to continue.");
                Transaction = false;
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
                agreement = Console.ReadLine();
            } while (agreement != "N" && agreement != "Y" && agreement != "n" && agreement != "y");
        }
    }
}
