using System;

namespace Bank
{
    class Communication
    {
        public void GetRemark()
        {
            Console.WriteLine("(Each card is linked to a separate account, if you want to link two cards, click on the 3rd section)");
        }

        public void GetInstruction()
        {
            Console.Write("1-Create debit card.\n2-Create credit card.\n3-Operations with your card.");
        }

        public void GetMessageAboutRegistration()
        {
            Console.WriteLine("\t--------Registration--------");
        }

        public void GetMessageAboutMoneyShortageError()
        {
            Console.WriteLine("You cannot withdraw more money than you have!\nBut you can take out a credit.\nPress any key to continue.");
        }

        public void GetVerificationInstruction()
        {
            Console.Write("Confirm registration (Enter)\nRegister again(Space)\nCancel and exit registration(Esc)");
        }

        public void GetMessageAboutError()
        {
            Console.WriteLine("You cannot transfer money from credit to debit!\nPress any key to continue.");
            Console.ReadKey();
        }

        public void GetInformationAboutUser(string name, string surname, int standartNumberOfDigits, params int[] numberOfCard)
        {
            Console.WriteLine("You are successfully logged in!");

            Console.Write($"Name: {name}\nSurname: {surname}\nNumber: ");

            for (int j = 0; j < standartNumberOfDigits; j++)
            {
                Console.Write(numberOfCard[j]);
            }

        }

        public void GetDebitCardMenuInstruction()
        {
            Console.Write("\n1-Deposit.\n2-Withdraw money.\n3-Transfer.\n4-Connect accounts of debit cards\n5-Exit.\n");
        }

        public void GetCreditCardMenuInstruction()
        {
            Console.Write("\n1-Deposit.\n2-Withdraw money.\n3-Transfer to a credit card.\n4-Take out a credit.\n5-Repay the credit.\n6-Connect accounts of cards\n7-Exit.\n");
        }

        public void GetMessageAboutTransfer()
        {
            Console.WriteLine("\nWhere will the transfer be made:");
        }

        public void GetMessageAboutSuccessfullyOperation()
        {
            Console.WriteLine("Operation was successfully completed.Press any key to continue.");
            Console.ReadKey();
        }

        public void GetMessageAboutWrongPassword()
        {
            Console.WriteLine("Wrong password or card number.\nPress any key to continue.");
            Console.ReadKey();
        }

        public void GetMessageAboutPayingOffCredit()
        {
            Console.WriteLine("Pay off your credit first!\nPress any key to continue.");
            Console.ReadKey();
        }

        public void GetMessageAboutUnknownAccount()
        {
            Console.WriteLine("Account not found! Press any key to return to the menu.");
            Console.ReadKey();
        }
    }
}
