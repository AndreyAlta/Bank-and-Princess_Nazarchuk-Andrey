using System;
using static System.Console;

namespace Bank
{
    class Action
    {
        private int indexFirstCard;
        private int indexSecondCard;
        private int counterNumbers;
        private int counterMax;
        private int counterMin;
        private int counterDebit;
        private int counterCredit;
        private int counterTruth;
        private int password;
        private int[] number;
        private bool exit;
        private bool errorTransaction;
        private bool registration;
        private bool creditTransaction;
        private bool unknown;
        private bool connectDebit = false;
        private bool connectCredit = false;
        private bool verification;
        private bool trueUser = true;
        private string agreement;
        private string choice;
        private double money;
        CreditCard[] creditCard = new CreditCard[0];
        DebitCard[] debitCard = new DebitCard[0];
        public void CreateDebitCard()
        {
            Array.Resize(ref debitCard, counterDebit + 1);
            debitCard[counterDebit] = new DebitCard();
            do
            {
                exit = false;
                WriteLine("----------Registration----------");
                Write("Write your surname: ");
                debitCard[counterDebit].Surname = ReadLine();
                Write("Write your name: ");
                debitCard[counterDebit].Name = ReadLine();
                counterTruth = 0;
                do
                {
                    InputNumberCard();
                    registration = true;
                    for (int i = 0; i < counterDebit; i++)
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            if (debitCard[i].number[j] == number[j])
                            {
                                counterTruth++;
                            }
                        }
                        if (counterTruth == 20)
                        {
                            registration = false;
                            WriteLine("This account number already exists");
                        }
                        counterTruth = 0;
                    }
                    for (int i = 0; i < 20; i++)
                    {
                        debitCard[counterDebit].number[i] = number[i];
                    }
                    counterDebit++;
                    CheckOriginality();
                    counterDebit--;
                } while (registration == false);
                counterNumbers = 0;
                do
                {
                    Write("Write your password(4 digits): ");
                } while (!int.TryParse(ReadLine(), out debitCard[counterDebit].password) || debitCard[counterDebit].password > 9999 || debitCard[counterDebit].password < 999);
                Verification();
                if (exit == true)
                {
                    debitCard[counterDebit].number = null;
                    return;
                }
                Clear();
            } while (verification == false);

            counterDebit++;
        }
        public void CreateCreditCard()
        {
            Array.Resize(ref creditCard, counterCredit + 1);
            creditCard[counterCredit] = new CreditCard();
            do
            {
                WriteLine("----------Registration----------");
                Write("Write your surname: ");
                creditCard[counterCredit].Surname = ReadLine();
                Write("Write your name: ");
                creditCard[counterCredit].Name = ReadLine();
                counterTruth = 0;
                do
                {
                    InputNumberCard();
                    registration = true;
                    for (int i = 0; i < counterCredit; i++)
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            if (creditCard[i].number[j] == number[j])
                            {
                                counterTruth++;
                            }
                        }
                        if (counterTruth == 20)
                        {
                            registration = false;
                            WriteLine("This account number already exists");
                        }
                        counterTruth = 0;
                    }
                    for (int i = 0; i < counterNumbers; i++)
                    {
                        creditCard[counterCredit].number[i] = number[i];
                    }
                    counterCredit++;
                    CheckOriginality();
                    counterCredit--;
                } while (registration == false);
                counterNumbers = 0;
                do
                {
                    Write("Write your password(4 digits): ");
                } while (!int.TryParse(ReadLine(), out creditCard[counterCredit].password) || creditCard[counterCredit].password > 9999 || creditCard[counterCredit].password < 999);
                Verification();
                if (exit == true)
                {
                    creditCard[counterCredit].number = null;
                    exit = false;
                    return;
                }
                Clear();
            } while (verification == false);
            counterCredit++;
        }
        public void Verification()
        {
            do
            {
                Write("\nConfirm registration (Y/y)\nRegister again(N/n)\nCancel and exit registration(E/e)\nChoose and write: ");
                choice = ReadLine();
            } while (choice != "Y" && choice != "y" && choice != "N" && choice != "n" && choice != "E" && choice != "e");
            switch (true)
            {
                case true when choice == "Y" | choice == "y":
                    verification = true;
                    break;
                case true when choice == "N" | choice == "n":
                    verification = false;
                    break;
                case true when choice == "E" | choice == "e":
                    exit = true;
                    break;
            }
        }
        public void GetInfo()
        {
            bool credit;
            bool entrance = false;
            InputNumberCard();
            if (trueUser == true || connectCredit == true || connectDebit == true)
            {
                do
                {
                    Write("Write your password(4 digits): ");
                } while (!int.TryParse(ReadLine(), out password) || password > 9999 || password < 999);
                Clear();
            }
            for (int i = 0; i < counterDebit; i++, counterTruth = 0)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (debitCard[i].number[j] == number[j])
                    {
                        counterTruth++;
                    }
                }
                switch (true)
                {
                    case true when trueUser == false && counterTruth == 20 && creditTransaction == false:
                        indexSecondCard = i;
                        counterTruth = 0;
                        return;
                    case true when trueUser == false && counterTruth == 20 && creditTransaction == true && connectCredit == false:
                        WriteLine("You cannot transfer money from credit to debit!\nPress any key to continue.");
                        ReadKey();
                        errorTransaction = true;
                        counterTruth = 0;
                        return;
                    case true when connectDebit == true && counterTruth == 20 && debitCard[i].password == password:
                        indexSecondCard = i;
                        counterTruth = 0;
                        return;

                }
                if (trueUser == true && counterTruth == 20 && creditTransaction == false)
                {
                    indexFirstCard = i;
                    break;
                }
            }
            if (connectDebit == true)
            {
                exit = true;
                return;
            }
            if (counterTruth == 20)
            {
                unknown = false;
                entrance = true;
                if (debitCard[indexFirstCard].password == password)
                {
                    do
                    {
                        Clear();
                        WriteLine("You are successfully logged in!");
                        Write($"Name: {debitCard[indexFirstCard].Name}\nSurname: {debitCard[indexFirstCard].Surname}\nNumber: ");
                        for (int j = 0; j < 20; j++)
                        {
                            Write(debitCard[indexFirstCard].number[j]);
                        }
                        debitCard[indexFirstCard].CheckMoney();
                        do
                        {
                            Write("\n1-Deposit.\n2-Withdraw money.\n3-Transfer.\n4-Connect accounts of debit cards\n5-Exit.\nChoose and write: ");
                            choice = ReadLine();
                        } while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5");
                        counterTruth = 0;
                        switch (choice)
                        {
                            case "1":
                                debitCard[indexFirstCard].AddMoney();
                                break;
                            case "2":
                                debitCard[indexFirstCard].WithdrawMoney();
                                break;
                            case "3":
                                creditTransaction = false;
                                trueUser = false;
                                WriteLine("\nWhere will the transfer be made:");
                                GetInfo();
                                trueUser = true;
                                Write("Enter the amount of money: ");
                                while (!double.TryParse(Console.ReadLine(), out money) || money < 0)
                                {
                                    Write("Enter the amount of money: ");
                                }
                                do
                                {
                                    Write("Are you sure of your action? (please write yes-Y(y) or no-N(n)): ");
                                    agreement = ReadLine();
                                } while (agreement != "N" && agreement != "Y" && agreement != "n" && agreement != "y");
                                if (agreement == "y" || agreement == "Y")
                                {
                                    if (unknown == false)
                                    {
                                        debitCard[indexFirstCard].WithdrawTransactionMoney(money);
                                        if (creditTransaction == false && debitCard[indexFirstCard].transaction == true)
                                        {
                                            debitCard[indexSecondCard].TransactionMoney(money);
                                            WriteLine("Operation was successfully completed.Press any key to continue.");
                                            ReadKey();
                                        }
                                        else if (creditTransaction == true && debitCard[indexFirstCard].transaction == true)
                                        {
                                            creditCard[indexSecondCard].AddTransactionMoney(money);
                                            WriteLine("Operation was successfully completed.Press any key to continue.");
                                            ReadKey();
                                            creditTransaction = false;
                                        }
                                    }
                                    else if (unknown == true)
                                    {
                                        debitCard[indexFirstCard].WithdrawTransactionMoney(money);
                                        WriteLine("Operation was successfully completed.Press any key to continue.");
                                        ReadKey();
                                        Clear();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                break;
                            case "4":
                                connectDebit = true;
                                GetInfo();
                                if (exit == false)
                                {
                                    debitCard[indexFirstCard].ConnectCards(debitCard[indexSecondCard].account);
                                    WriteLine("Operation was successfully completed.Press any key to continue.");
                                    ReadKey();
                                }
                                else
                                {
                                    WriteLine("Wrong password or card number.\nPress any key to continue.");
                                    ReadKey();
                                    exit = false;
                                }
                                connectDebit = false;
                                break;
                            case "5":
                                return;
                        }
                    } while (choice != "5");
                }
                else
                {
                    WriteLine("Wrong password.\nPress any key.");
                    ReadKey();
                }
            }
            for (int i = 0; i < counterCredit; i++, counterTruth = 0)
            {
                counterTruth = 0;
                for (int j = 0; j < 20; j++)
                {
                    if (creditCard[i].number[j] == number[j])
                    {
                        counterTruth++;
                    }
                }
                switch (true)
                {
                    case true when trueUser == false && counterTruth == 20 && creditTransaction == true:
                        indexSecondCard = i;
                        counterTruth = 0;
                        return;
                    case true when trueUser == false && counterTruth == 20 && creditTransaction == false:
                        creditTransaction = true;
                        indexSecondCard = i;
                        counterTruth = 0;
                        return;
                    case true when connectCredit == true && counterTruth == 20 && creditCard[i].password == password:
                        indexSecondCard = i;
                        counterTruth = 0;
                        return;

                }
                if (trueUser == true && counterTruth == 20 && creditTransaction == false)
                {
                    indexFirstCard = i;
                    break;
                }
            }
            if (connectCredit == true)
            {
                exit = true;
                return;
            }
            if (trueUser == false && counterTruth != 20)
            {
                Write("Write the name of the recipient: ");
                ReadLine();
                Write("Write the surnname of the recipient: ");
                ReadLine();
                unknown = true;
                return;
            }
            if (counterTruth == 20)
            {
                unknown = false;
                entrance = true;
                if (creditCard[indexFirstCard].password == password)
                {
                    do
                    {
                        Clear();
                        WriteLine("You are successfully logged in!");
                        Write($"Name: {creditCard[indexFirstCard].Name}\nSurname: {creditCard[indexFirstCard].Surname}\nNumber: ");
                        for (int j = 0; j < 20; j++)
                        {
                            Write(creditCard[indexFirstCard].number[j]);
                        }
                        creditCard[indexFirstCard].CheckMoney();
                        creditCard[indexFirstCard].ShowCredit();
                        do
                        {
                            Write("\n1-Deposit.\n2-Withdraw money.\n3-Transfer to a credit card.\n4-Take out a credit.\n5-Repay the credit.\n6-Connect accounts of cards\n7-Exit.\nChoose and write: ");
                            choice = ReadLine();
                        } while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5" && choice != "6" && choice != "7");
                        counterTruth = 0;
                        switch (choice)
                        {
                            case "1":
                                creditCard[indexFirstCard].AddMoney();
                                break;
                            case "2":
                                creditCard[indexFirstCard].WithdrawMoney();
                                break;
                            case "3":
                                credit = creditCard[indexFirstCard].CheckCredit();
                                if (credit == false)
                                {
                                    trueUser = false;
                                    errorTransaction = false;
                                    WriteLine("\nWhere will the transfer be made:");
                                    creditTransaction = true;
                                    GetInfo();
                                    trueUser = true;
                                    creditTransaction = false;
                                    if (errorTransaction == false)
                                    {
                                        Write("Enter the amount of money: ");
                                        while (!double.TryParse(ReadLine(), out money) || money < 0)
                                        {
                                            Write("Enter the amount of money: ");
                                        }
                                        do
                                        {
                                            Write("Are you sure of your action? (please write yes-Y(y) or no-N(n)): ");
                                            agreement = ReadLine();
                                        } while (agreement != "N" && agreement != "Y" && agreement != "n" && agreement != "y");
                                        if (agreement == "Y" || agreement == "y")
                                        {
                                            if (unknown == false)
                                            {
                                                creditCard[indexFirstCard].WithdrawTransactionMoney(money);
                                                if (creditCard[indexFirstCard].transaction == true)
                                                {
                                                    creditCard[indexSecondCard].AddTransactionMoney(money);
                                                    WriteLine("Operation was successfully completed.Press any key to continue.");
                                                    ReadKey();
                                                }
                                            }
                                            else if (unknown == true)
                                            {
                                                creditCard[indexFirstCard].WithdrawTransactionMoney(money);
                                                WriteLine("Operation was successfully completed.Press any key to continue.");
                                                ReadKey();
                                                Clear();
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    errorTransaction = false;
                                }
                                else
                                {
                                    Clear();
                                    WriteLine("Pay off your credit first!\nPress any key to continue.");
                                    ReadKey();
                                }
                                break;
                            case "4":
                                creditCard[indexFirstCard].AddCredit();
                                break;
                            case "5":
                                creditCard[indexFirstCard].WithdrawCredit();
                                break;
                            case "6":
                                connectCredit = true;
                                trueUser = false;
                                creditTransaction = true;
                                GetInfo();
                                creditTransaction = false;
                                connectCredit = false;
                                trueUser = true;
                                if (exit == false)
                                {
                                    connectCredit = false;
                                    creditCard[indexFirstCard].ConnectCards(creditCard[indexSecondCard].account);
                                    WriteLine("Operation was successfully completed.Press any key to continue.");
                                    ReadKey();
                                }
                                else
                                {
                                    WriteLine("Wrong password or card number.\nPress any key to continue.");
                                    ReadKey();
                                    exit = false;
                                }
                                connectCredit = false;
                                break;
                            case "7":
                                return;
                        }
                    } while (choice != "7");
                }
                else
                {
                    WriteLine("Wrong password.\nPress any key.");
                    ReadKey();
                }
            }
            if (entrance == false)
            {
                WriteLine("Account not found! Press any key to return to the menu.");
                ReadKey();
                return;
            }
        }
        public void InputNumberCard()
        {
            bool numberParse;
            do
            {
                Write("Write number of card(20 signs): ");
                string numberString = ReadLine();
                char[] numberChar = numberString.ToCharArray();
                number = new int[numberChar.Length];
                counterNumbers = numberChar.Length;
                numberParse = true;
                for (int i = 0; i < numberChar.Length; i++)
                {
                    if (!int.TryParse(numberChar[i].ToString(), out number[i]))
                    {
                        numberParse = false;
                    }
                }
            } while (numberParse == false || counterNumbers != 20);
        }
        public void CheckOriginality()
        {
            if (counterDebit < counterCredit)
            {
                counterMax = counterCredit;
                counterMin = counterDebit;
            }
            else
            {
                counterMax = counterDebit;
                counterMin = counterCredit;
            }
            for (int k = 0; k < counterMin; k++)
            {
                for (int i = 0; i < counterMax; i++)
                {
                    if (counterDebit < counterCredit)
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            if (debitCard[k].number == null || creditCard[i].number == null)
                            {
                                break;
                            }
                            if (debitCard[k].number[j] == creditCard[i].number[j])
                            {
                                counterTruth++;
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            if (debitCard[i].number == null || creditCard[k].number == null)
                            {
                                break;
                            }
                            if (debitCard[i].number[j] == creditCard[k].number[j])
                            {
                                counterTruth++;
                            }
                        }
                    }
                    if (counterTruth == 20)
                    {
                        registration = false;
                        WriteLine("This account number already exists");
                        counterTruth = 0;
                        return;
                    }
                    counterTruth = 0;
                }
            }
        }
    }
}
