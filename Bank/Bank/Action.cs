﻿using System;
using static System.Console;

namespace Bank
{
    class Action
    {
        public const ConsoleKey UnknownKey = 0;

        private ConsoleKey key;

        private const int StandartNumberOfDigits = 20;

        private int indexFirstCard;
        private int indexSecondCard;

        private int counterNumbers;

        private int counterMaxNumberOfCard;
        private int counterMinNumberOfCard;

        private int counterDebit;
        private int counterCredit;

        private int counterСoincidence;

        private string password;

        private int[] number;

        private bool isCancelSelection;

        private bool isErrorTransaction;

        private bool isRegistrationSuccessfuly;

        private bool isCreditTransaction;

        private bool isUnknownUser;

        private bool isConnectDebit;

        private bool isConnectCredit;

        private bool isTrueUser;

        private double money;

        CreditCard[] creditCard;
        DebitCard[] debitCard;

        Communication communication;

        public Action()
        {
            creditCard = new CreditCard[0];
            debitCard = new DebitCard[0];

            communication = new Communication();

            isTrueUser = true;

            isConnectCredit = false;

            isConnectDebit = false;

            isCancelSelection = false;
        }

        public void CreateDebitCard()
        {
            Console.Clear();
            Array.Resize(ref debitCard, counterDebit + 1);

            debitCard[counterDebit] = new DebitCard();

            do
            {
                communication.GetMessageAboutRegistration();

                Write("Write your surname: ");
                debitCard[counterDebit].Surname = ReadLine();

                Write("Write your name: ");
                debitCard[counterDebit].Name = ReadLine();

                counterСoincidence = 0;

                do
                {
                    InputNumberCard();

                    isRegistrationSuccessfuly = true;

                    for (int numberOfCard = 0; numberOfCard < counterDebit; numberOfCard++)
                    {

                        for (int numberOfDigits = 0; numberOfDigits < StandartNumberOfDigits; numberOfDigits++)
                        {
                            if (debitCard[numberOfCard].Number[numberOfDigits] == number[numberOfDigits])
                            {
                                counterСoincidence++;
                            }
                        }

                        if (counterСoincidence == StandartNumberOfDigits)
                        {
                            isRegistrationSuccessfuly = false;
                            WriteLine("This account number already exists");
                        }

                        counterСoincidence = 0;
                    }

                    for (int i = 0; i < StandartNumberOfDigits; i++)
                    {
                        debitCard[counterDebit].Number[i] = number[i];
                    }

                    counterDebit++;
                    CheckOriginality();
                    counterDebit--;
                }
                while (isRegistrationSuccessfuly == false);

                counterNumbers = 0;

                InputPassword();

                debitCard[counterDebit].Password = password;
                Verification();

                if (key == ConsoleKey.Escape)
                {
                    debitCard[counterDebit].Number = null;
                    return;
                }

                Clear();
            }
            while (key == ConsoleKey.Spacebar);

            counterDebit++;
        }

        public void CreateCreditCard()
        {
            Console.Clear();
            Array.Resize(ref creditCard, counterCredit + 1);

            creditCard[counterCredit] = new CreditCard();

            do
            {
                communication.GetMessageAboutRegistration();

                Write("Write your surname: ");
                creditCard[counterCredit].Surname = ReadLine();

                Write("Write your name: ");
                creditCard[counterCredit].Name = ReadLine();

                counterСoincidence = 0;

                do
                {
                    InputNumberCard();

                    isRegistrationSuccessfuly = true;

                    for (int numberOfCard = 0; numberOfCard < counterCredit; numberOfCard++)
                    {

                        for (int numberOfDigits = 0; numberOfDigits < StandartNumberOfDigits; numberOfDigits++)
                        {
                            if (creditCard[numberOfCard].Number[numberOfDigits] == number[numberOfDigits])
                            {
                                counterСoincidence++;
                            }
                        }

                        if (counterСoincidence == StandartNumberOfDigits)
                        {
                            isRegistrationSuccessfuly = false;
                            WriteLine("This account number already exists");
                        }

                        counterСoincidence = 0;

                    }

                    for (int i = 0; i < counterNumbers; i++)
                    {
                        creditCard[counterCredit].Number[i] = number[i];
                    }

                    counterCredit++;

                    CheckOriginality();

                    counterCredit--;
                }
                while (isRegistrationSuccessfuly == false);

                counterNumbers = 0;

                InputPassword();

                creditCard[counterCredit].Password = password;

                Verification();

                if (key == ConsoleKey.Escape)
                {
                    creditCard[counterCredit].Number = null;
                    return;
                }

                Clear();
            }
            while (key == ConsoleKey.Spacebar);

            counterCredit++;
        }

        public void Verification()
        {
            do
            {
                Console.Clear();

                communication.GetVerificationInstruction();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        key = ConsoleKey.Enter;
                        break;
                    case ConsoleKey.Spacebar:
                        key = ConsoleKey.Spacebar;
                        break;
                    case ConsoleKey.Escape:
                        key = ConsoleKey.Escape;
                        break;
                    default:
                        key = UnknownKey;
                        break;
                }
            }
            while (key == UnknownKey);
        }

        public void GetInfo()
        {
            Console.Clear();

            bool loggedIn = false;

            InputNumberCard();

            if (isTrueUser == true || isConnectCredit == true || isConnectDebit == true)
            {
                InputPassword();

                Clear();
            }

            for (int numberOfCard = 0; numberOfCard < counterDebit; numberOfCard++, counterСoincidence = 0)
            {

                for (int numberOfDigits = 0; numberOfDigits < StandartNumberOfDigits; numberOfDigits++)
                {
                    if (debitCard[numberOfCard].Number[numberOfDigits] == number[numberOfDigits])
                    {
                        counterСoincidence++;
                    }
                }

                switch (true)
                {
                    case true when isTrueUser == false && counterСoincidence == StandartNumberOfDigits && isCreditTransaction == false:

                        indexSecondCard = numberOfCard;

                        counterСoincidence = 0;
                        return;
                    case true when isTrueUser == false && counterСoincidence == StandartNumberOfDigits && isCreditTransaction == true && isConnectCredit == false:

                        communication.GetMessageAboutError();

                        isErrorTransaction = true;

                        counterСoincidence = 0;
                        return;
                    case true when isConnectDebit == true && counterСoincidence == StandartNumberOfDigits && debitCard[numberOfCard].Password == password:

                        indexSecondCard = numberOfCard;

                        counterСoincidence = 0;
                        return;
                    case true when isConnectDebit == true && counterСoincidence != StandartNumberOfDigits || debitCard[numberOfCard].Password != password:

                        isConnectDebit = false;
                        return;
                }

                if (isTrueUser == true && counterСoincidence == StandartNumberOfDigits && isCreditTransaction == false)
                {
                    indexFirstCard = numberOfCard;
                    break;
                }
            }
            if (counterСoincidence == StandartNumberOfDigits)
            {
                isUnknownUser = false;
                loggedIn = true;

                if (debitCard[indexFirstCard].Password == password)
                {
                    do
                    {
                        Clear();

                        communication.GetInformationAboutUser(debitCard[indexFirstCard].Name, debitCard[indexFirstCard].Surname, StandartNumberOfDigits, debitCard[indexFirstCard].Number);

                        debitCard[indexFirstCard].CheckMoney();

                        communication.GetDebitCardMenuInstruction();

                        counterСoincidence = 0;

                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.NumPad1:

                                debitCard[indexFirstCard].AddMoney();
                                break;
                            case ConsoleKey.NumPad2:

                                debitCard[indexFirstCard].WithdrawMoney();
                                break;
                            case ConsoleKey.NumPad3:

                                Console.Clear();

                                isCreditTransaction = false;

                                isTrueUser = false;

                                communication.GetMessageAboutTransfer();

                                GetInfo();

                                isTrueUser = true;

                                do
                                {
                                    Write("Enter the amount of money: ");
                                }
                                while (!double.TryParse(Console.ReadLine(), out money) || money < 0);

                                Write("Are you sure of your action? (please choose Enter or Esc: ");
                                do
                                {
                                    switch (Console.ReadKey().Key)
                                    {
                                        case ConsoleKey.Enter:
                                            key = ConsoleKey.Enter;

                                            if (isUnknownUser == false)
                                            {

                                                debitCard[indexFirstCard].WithdrawTransactionMoney(money);

                                                if (isCreditTransaction == false && debitCard[indexFirstCard].Transaction == true)
                                                {
                                                    debitCard[indexSecondCard].TransactionMoney(money);

                                                    communication.GetMessageAboutSuccessfullyOperation();
                                                }

                                                else if (isCreditTransaction == true && debitCard[indexFirstCard].Transaction == true)
                                                {
                                                    creditCard[indexSecondCard].AddTransactionMoney(money);

                                                    communication.GetMessageAboutSuccessfullyOperation();

                                                    isCreditTransaction = false;
                                                }
                                            }

                                            else if (isUnknownUser == true)
                                            {
                                                debitCard[indexFirstCard].WithdrawTransactionMoney(money);

                                                communication.GetMessageAboutSuccessfullyOperation();

                                                Clear();
                                            }
                                            break;
                                        case ConsoleKey.Escape:
                                            key = ConsoleKey.Escape;
                                            break;
                                        default:
                                            key = UnknownKey;
                                            break;
                                    }
                                }
                                while (key == UnknownKey);
                                isCreditTransaction = false;
                                break;
                            case ConsoleKey.NumPad4:
                                isConnectDebit = true;

                                GetInfo();

                                if (isConnectDebit == true)
                                {
                                    isConnectDebit = false;

                                    debitCard[indexFirstCard].ConnectCards(debitCard[indexSecondCard].Account);

                                    communication.GetMessageAboutSuccessfullyOperation();
                                }
                                else
                                {
                                    communication.GetMessageAboutWrongPassword();

                                }

                                isConnectDebit = false;
                                break;
                            case ConsoleKey.NumPad5:

                                isCancelSelection = true;
                                return;
                        }
                        Console.Clear();
                    }
                    while (isCancelSelection == false);

                    isCancelSelection = false;
                }
                else
                {
                    communication.GetMessageAboutWrongPassword();
                }

            }

            for (int i = 0; i < counterCredit; i++, counterСoincidence = 0)
            {

                counterСoincidence = 0;

                for (int j = 0; j < StandartNumberOfDigits; j++)
                {
                    if (creditCard[i].Number[j] == number[j])
                    {
                        counterСoincidence++;
                    }
                }

                switch (true)
                {
                    case true when isTrueUser == false && counterСoincidence == StandartNumberOfDigits && isCreditTransaction == true:

                        indexSecondCard = i;

                        counterСoincidence = 0;
                        return;
                    case true when isTrueUser == false && counterСoincidence == StandartNumberOfDigits && isCreditTransaction == false:

                        isCreditTransaction = true;

                        indexSecondCard = i;

                        counterСoincidence = 0;
                        return;
                    case true when isConnectCredit == true && counterСoincidence == StandartNumberOfDigits && creditCard[i].Password == password:

                        indexSecondCard = i;

                        counterСoincidence = 0;
                        return;
                    case true when isConnectCredit == true && counterСoincidence != StandartNumberOfDigits || creditCard[i].Password != password:

                        isConnectCredit = false;
                        return;

                }

                if (isTrueUser == true && counterСoincidence == StandartNumberOfDigits && isCreditTransaction == false)
                {
                    indexFirstCard = i;
                    break;
                }

            }

            if (isConnectCredit == true)
            {
                return;
            }

            if (isTrueUser == false && counterСoincidence != StandartNumberOfDigits)
            {
                Write("Write the name of the recipient: ");
                ReadLine();

                Write("Write the surnname of the recipient: ");
                ReadLine();

                isUnknownUser = true;
                return;
            }

            if (counterСoincidence == StandartNumberOfDigits)
            {
                isUnknownUser = false;

                loggedIn = true;

                if (creditCard[indexFirstCard].Password == password)
                {
                    do
                    {
                        Clear();

                        communication.GetInformationAboutUser(debitCard[indexFirstCard].Name, debitCard[indexFirstCard].Surname, StandartNumberOfDigits, debitCard[indexFirstCard].Number);

                        creditCard[indexFirstCard].CheckMoney();

                        creditCard[indexFirstCard].ShowCredit();

                        communication.GetCreditCardMenuInstruction();

                        communication.GetCreditCardMenuInstruction();

                        counterСoincidence = 0;

                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.NumPad1:

                                creditCard[indexFirstCard].AddMoney();

                                break;
                            case ConsoleKey.NumPad2:

                                creditCard[indexFirstCard].WithdrawMoney();

                                break;
                            case ConsoleKey.NumPad3:

                                if (creditCard[indexFirstCard].CheckCredit())
                                {
                                    isTrueUser = false;

                                    isErrorTransaction = false;

                                    communication.GetMessageAboutTransfer();

                                    isCreditTransaction = true;

                                    GetInfo();

                                    isTrueUser = true;

                                    isCreditTransaction = false;

                                    if (isErrorTransaction == false)
                                    {
                                        do
                                        {
                                            Write("Enter the amount of money: ");
                                        }
                                        while (!double.TryParse(ReadLine(), out money) || money < 0);

                                        Write("Are you sure of your action? (please write yes-Y(y) or no-N(n)): ");

                                        do
                                        {
                                            switch (Console.ReadKey().Key)
                                            {
                                                case ConsoleKey.Enter:

                                                    if (isUnknownUser == false)
                                                    {
                                                        creditCard[indexFirstCard].WithdrawTransactionMoney(money);

                                                        if (creditCard[indexFirstCard].Transaction == true)
                                                        {
                                                            creditCard[indexSecondCard].AddTransactionMoney(money);

                                                            communication.GetMessageAboutSuccessfullyOperation();
                                                        }
                                                    }
                                                    else if (isUnknownUser == true)
                                                    {
                                                        creditCard[indexFirstCard].WithdrawTransactionMoney(money);

                                                        communication.GetMessageAboutSuccessfullyOperation();

                                                        Clear();
                                                    }
                                                    break;
                                                case ConsoleKey.Escape:
                                                    key = ConsoleKey.Escape;
                                                    break;
                                                default:
                                                    key = UnknownKey;
                                                    break;
                                            }
                                        }
                                        while (key == UnknownKey);

                                        isErrorTransaction = false;
                                    }
                                    else
                                    {
                                        Clear();

                                        communication.GetMessageAboutPayingOffCredit();
                                    }
                                }
                                break;
                            case ConsoleKey.NumPad4:

                                creditCard[indexFirstCard].AddCredit();

                                break;
                            case ConsoleKey.NumPad5:

                                creditCard[indexFirstCard].WithdrawCredit();

                                break;
                            case ConsoleKey.NumPad6:

                                isConnectCredit = true;

                                isTrueUser = false;

                                isCreditTransaction = true;

                                GetInfo();

                                isCreditTransaction = false;

                                isTrueUser = true;

                                if (isConnectCredit == true)
                                {
                                    isConnectCredit = false;

                                    creditCard[indexFirstCard].ConnectCards(creditCard[indexSecondCard].Account);

                                    communication.GetMessageAboutSuccessfullyOperation();
                                }

                                else
                                {
                                    communication.GetMessageAboutWrongPassword();

                                }

                                isConnectCredit = false;

                                break;
                            case ConsoleKey.NumPad7:

                                isCancelSelection = true;

                                return;
                        }
                    }
                    while (isCancelSelection == false);

                    isCancelSelection = false;
                }
                else
                {
                    communication.GetMessageAboutWrongPassword();
                }
            }

            if (loggedIn == false)
            {
                communication.GetMessageAboutUnknownAccount();
                return;
            }

        }

        public void InputPassword()
        {
            do
            {
                Write("Write your password(4 digits): ");
                password = Console.ReadLine();
            }
            while (!int.TryParse(password, out int value) || password.Length != 4);
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

            }
            while (numberParse == false || counterNumbers != StandartNumberOfDigits);
        }

        public void CheckOriginality()
        {
            int numberOfTheFirstTypeOfCard = 0;

            int numberOfTheSecondTypeOfCard = 0;

            int temp;

            if (counterDebit < counterCredit)
            {
                counterMaxNumberOfCard = counterCredit;
                counterMinNumberOfCard = counterDebit;
            }

            else
            {
                counterMaxNumberOfCard = counterDebit;
                counterMinNumberOfCard = counterCredit;

                temp = numberOfTheFirstTypeOfCard;

                numberOfTheFirstTypeOfCard = numberOfTheSecondTypeOfCard;

                numberOfTheSecondTypeOfCard = temp;
            }

            for (numberOfTheFirstTypeOfCard = 0; numberOfTheFirstTypeOfCard < counterMinNumberOfCard; numberOfTheFirstTypeOfCard++)
            {

                for (numberOfTheSecondTypeOfCard = 0; numberOfTheSecondTypeOfCard < counterMaxNumberOfCard; numberOfTheSecondTypeOfCard++)
                {

                    for (int numberOfDigits = 0; numberOfDigits < StandartNumberOfDigits; numberOfDigits++)
                    {

                        if (debitCard[numberOfTheFirstTypeOfCard].Number == null || creditCard[numberOfTheSecondTypeOfCard].Number == null)
                        {
                            break;
                        }

                        if (debitCard[numberOfTheFirstTypeOfCard].Number[numberOfDigits] == creditCard[numberOfTheSecondTypeOfCard].Number[numberOfDigits])
                        {
                            counterСoincidence++;
                        }

                    }

                    if (counterСoincidence == StandartNumberOfDigits)
                    {
                        isRegistrationSuccessfuly = false;

                        WriteLine("This account number already exists");

                        counterСoincidence = 0;

                        return;
                    }

                    counterСoincidence = 0;
                }
            }
        }
    }
}
