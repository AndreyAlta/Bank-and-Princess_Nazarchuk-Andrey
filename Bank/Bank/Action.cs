using System;
using static System.Console;

namespace Bank
{
    class Action
    {
        public const ConsoleKey UnknownKey = 0;

        private ConsoleKey Key;

        private const int StandartNumberOfDigitsInCardNumber = 20;

        private const int StandartNumberOfDigitsInPassword = 4;

        private int IndexFirstCard;
        private int IndexSecondCard;

        private int CounterNumbers;

        private int CounterMaxNumberOfCard;
        private int CounterMinNumberOfCard;

        private int CounterDebit;
        private int CounterCredit;

        private int CounterСoincidence;

        private string Password;

        private int[] Number;

        private bool IsCancelSelection;

        private bool IsErrorTransaction;

        private bool IsRegistrationSuccessfuly;

        private bool IsCreditTransaction;

        private bool IsUnknownUser;

        private bool IsConnectDebit;

        private bool IsConnectCredit;

        private bool IsTrueUser;

        private double Money;

        Card[] CreditCard;

        Card[] DebitCard;

        Communication Communication;

        public Action()
        {
            CreditCard = new Card[0];

            DebitCard = new Card[0];

            Communication = new Communication();

            IsTrueUser = true;

            IsConnectCredit = false;

            IsConnectDebit = false;

            IsCancelSelection = false;
        }

        public void CreateDebitCard()
        {
            Console.Clear();
            Array.Resize(ref DebitCard, CounterDebit + 1);

            DebitCard[CounterDebit] = new Card();

            do
            {
                Communication.GetMessageAboutRegistration();

                Write("Write your surname: ");
                DebitCard[CounterDebit].Surname = ReadLine();

                Write("Write your name: ");
                DebitCard[CounterDebit].Name = ReadLine();

                CounterСoincidence = 0;

                do
                {
                    InputNumberCard();

                    IsRegistrationSuccessfuly = true;

                    for (int numberOfCard = 0; numberOfCard < CounterDebit; numberOfCard++)
                    {

                        for (int numberOfDigits = 0; numberOfDigits < StandartNumberOfDigitsInCardNumber; numberOfDigits++)
                        {
                            if (DebitCard[numberOfCard].Number[numberOfDigits] == Number[numberOfDigits])
                            {
                                CounterСoincidence++;
                            }
                        }

                        if (CounterСoincidence == StandartNumberOfDigitsInCardNumber)
                        {
                            IsRegistrationSuccessfuly = false;
                            WriteLine("This account number already exists");
                        }

                        CounterСoincidence = 0;
                    }

                    for (int i = 0; i < StandartNumberOfDigitsInCardNumber; i++)
                    {
                        DebitCard[CounterDebit].Number[i] = Number[i];
                    }

                    CounterDebit++;
                    CheckOriginality();
                    CounterDebit--;
                }
                while (IsRegistrationSuccessfuly == false);

                CounterNumbers = 0;

                InputPassword();

                DebitCard[CounterDebit].Password = Password;
                Verification();

                if (Key == ConsoleKey.Escape)
                {
                    DebitCard[CounterDebit].Number = null;
                    return;
                }

                Clear();
            }
            while (Key == ConsoleKey.Spacebar);

            CounterDebit++;
        }

        public void CreateCreditCard()
        {
            Console.Clear();
            Array.Resize(ref CreditCard, CounterCredit + 1);

            CreditCard[CounterCredit] = new Card();

            do
            {
                Communication.GetMessageAboutRegistration();

                Write("Write your surname: ");
                CreditCard[CounterCredit].Surname = ReadLine();

                Write("Write your name: ");
                CreditCard[CounterCredit].Name = ReadLine();

                CounterСoincidence = 0;

                do
                {
                    InputNumberCard();

                    IsRegistrationSuccessfuly = true;

                    for (int numberOfCard = 0; numberOfCard < CounterCredit; numberOfCard++)
                    {

                        for (int numberOfDigits = 0; numberOfDigits < StandartNumberOfDigitsInCardNumber; numberOfDigits++)
                        {
                            if (CreditCard[numberOfCard].Number[numberOfDigits] == Number[numberOfDigits])
                            {
                                CounterСoincidence++;
                            }
                        }

                        if (CounterСoincidence == StandartNumberOfDigitsInCardNumber)
                        {
                            IsRegistrationSuccessfuly = false;
                            WriteLine("This account number already exists");
                        }

                        CounterСoincidence = 0;

                    }

                    for (int i = 0; i < CounterNumbers; i++)
                    {
                        CreditCard[CounterCredit].Number[i] = Number[i];
                    }

                    CounterCredit++;

                    CheckOriginality();

                    CounterCredit--;
                }
                while (IsRegistrationSuccessfuly == false);

                CounterNumbers = 0;

                InputPassword();

                CreditCard[CounterCredit].Password = Password;

                Verification();

                if (Key == ConsoleKey.Escape)
                {
                    CreditCard[CounterCredit].Number = null;
                    return;
                }

                Clear();
            }
            while (Key == ConsoleKey.Spacebar);

            CounterCredit++;
        }

        public void Verification()
        {
            do
            {
                Console.Clear();

                Communication.GetVerificationInstruction();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                        Key = ConsoleKey.Enter;
                        break;
                    case ConsoleKey.Spacebar:
                        Key = ConsoleKey.Spacebar;
                        break;
                    case ConsoleKey.Escape:
                        Key = ConsoleKey.Escape;
                        break;
                    default:
                        Key = UnknownKey;
                        break;
                }
            }
            while (Key == UnknownKey);
        }

        public void GetInfo()
        {
            Console.Clear();

            bool loggedIn = false;

            InputNumberCard();

            if (IsTrueUser == true || IsConnectCredit == true || IsConnectDebit == true)
            {
                InputPassword();

                Clear();
            }

            for (int numberOfCard = 0; numberOfCard < CounterDebit; numberOfCard++, CounterСoincidence = 0)
            {

                for (int numberOfDigits = 0; numberOfDigits < StandartNumberOfDigitsInCardNumber; numberOfDigits++)
                {
                    if (DebitCard[numberOfCard].Number[numberOfDigits] == Number[numberOfDigits])
                    {
                        CounterСoincidence++;
                    }
                }

                switch (true)
                {
                    case true when IsTrueUser == false && CounterСoincidence == StandartNumberOfDigitsInCardNumber && IsCreditTransaction == false:

                        IndexSecondCard = numberOfCard;

                        CounterСoincidence = 0;
                        return;
                    case true when IsTrueUser == false && CounterСoincidence == StandartNumberOfDigitsInCardNumber && IsCreditTransaction == true && IsConnectCredit == false:

                        Communication.GetMessageAboutError();

                        IsErrorTransaction = true;

                        CounterСoincidence = 0;
                        return;
                    case true when IsConnectDebit == true && CounterСoincidence == StandartNumberOfDigitsInCardNumber && DebitCard[numberOfCard].Password == Password:

                        IndexSecondCard = numberOfCard;

                        CounterСoincidence = 0;
                        return;
                    case true when IsConnectDebit == true && CounterСoincidence != StandartNumberOfDigitsInCardNumber || DebitCard[numberOfCard].Password != Password:

                        IsConnectDebit = false;
                        return;
                }

                if (IsTrueUser == true && CounterСoincidence == StandartNumberOfDigitsInCardNumber && IsCreditTransaction == false)
                {
                    IndexFirstCard = numberOfCard;
                    break;
                }
            }
            if (CounterСoincidence == StandartNumberOfDigitsInCardNumber)
            {
                IsUnknownUser = false;
                loggedIn = true;

                if (DebitCard[IndexFirstCard].Password == Password)
                {
                    do
                    {
                        Clear();

                        Communication.GetInformationAboutUser(DebitCard[IndexFirstCard].Name, DebitCard[IndexFirstCard].Surname, StandartNumberOfDigitsInCardNumber, DebitCard[IndexFirstCard].Number);

                        DebitCard[IndexFirstCard].CheckMoney();

                        Communication.GetDebitCardMenuInstruction();

                        CounterСoincidence = 0;

                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.NumPad1:

                                DebitCard[IndexFirstCard].AddMoney();
                                break;
                            case ConsoleKey.NumPad2:

                                DebitCard[IndexFirstCard].WithdrawMoney();
                                break;
                            case ConsoleKey.NumPad3:

                                Console.Clear();

                                IsCreditTransaction = false;

                                IsTrueUser = false;

                                Communication.GetMessageAboutTransfer();

                                GetInfo();

                                IsTrueUser = true;

                                do
                                {
                                    Write("Enter the amount of money: ");
                                }
                                while (!double.TryParse(Console.ReadLine(), out Money) || Money < 0);

                                Write("Are you sure of your action? (please choose Enter or Esc: ");
                                do
                                {
                                    switch (Console.ReadKey().Key)
                                    {
                                        case ConsoleKey.Enter:
                                            Key = ConsoleKey.Enter;

                                            if (IsUnknownUser == false)
                                            {

                                                DebitCard[IndexFirstCard].WithdrawTransactionMoney(Money);

                                                if (IsCreditTransaction == false && DebitCard[IndexFirstCard].TransactionCompleted == true)
                                                {
                                                    DebitCard[IndexSecondCard].TransactionMoney(Money);

                                                    Communication.GetMessageAboutSuccessfullyOperation();
                                                }

                                                else if (IsCreditTransaction == true && DebitCard[IndexFirstCard].TransactionCompleted == true)
                                                {
                                                    CreditCard[IndexSecondCard].AddTransactionMoney(Money);

                                                    Communication.GetMessageAboutSuccessfullyOperation();

                                                    IsCreditTransaction = false;
                                                }
                                            }

                                            else if (IsUnknownUser == true)
                                            {
                                                DebitCard[IndexFirstCard].WithdrawTransactionMoney(Money);

                                                Communication.GetMessageAboutSuccessfullyOperation();

                                                Clear();
                                            }
                                            break;
                                        case ConsoleKey.Escape:
                                            Key = ConsoleKey.Escape;
                                            break;
                                        default:
                                            Key = UnknownKey;
                                            break;
                                    }
                                }
                                while (Key == UnknownKey);
                                IsCreditTransaction = false;
                                break;
                            case ConsoleKey.NumPad4:
                                IsConnectDebit = true;

                                GetInfo();

                                if (IsConnectDebit == true)
                                {
                                    IsConnectDebit = false;

                                    DebitCard[IndexFirstCard].ConnectCards(DebitCard[IndexSecondCard].Account);

                                    Communication.GetMessageAboutSuccessfullyOperation();
                                }
                                else
                                {
                                    Communication.GetMessageAboutWrongPassword();

                                }

                                IsConnectDebit = false;
                                break;
                            case ConsoleKey.NumPad5:

                                IsCancelSelection = true;
                                return;
                        }
                        Console.Clear();
                    }
                    while (IsCancelSelection == false);

                    IsCancelSelection = false;
                }
                else
                {
                    Communication.GetMessageAboutWrongPassword();
                }

            }

            for (int i = 0; i < CounterCredit; i++, CounterСoincidence = 0)
            {

                CounterСoincidence = 0;

                for (int j = 0; j < StandartNumberOfDigitsInCardNumber; j++)
                {
                    if (CreditCard[i].Number[j] == Number[j])
                    {
                        CounterСoincidence++;
                    }
                }

                switch (true)
                {
                    case true when IsTrueUser == false && CounterСoincidence == StandartNumberOfDigitsInCardNumber && IsCreditTransaction == true:

                        IndexSecondCard = i;

                        CounterСoincidence = 0;
                        return;
                    case true when IsTrueUser == false && CounterСoincidence == StandartNumberOfDigitsInCardNumber && IsCreditTransaction == false:

                        IsCreditTransaction = true;

                        IndexSecondCard = i;

                        CounterСoincidence = 0;
                        return;
                    case true when IsConnectCredit == true && CounterСoincidence == StandartNumberOfDigitsInCardNumber && CreditCard[i].Password == Password:

                        IndexSecondCard = i;

                        CounterСoincidence = 0;
                        return;
                    case true when IsConnectCredit == true && CounterСoincidence != StandartNumberOfDigitsInCardNumber || CreditCard[i].Password != Password:

                        IsConnectCredit = false;
                        return;

                }

                if (IsTrueUser == true && CounterСoincidence == StandartNumberOfDigitsInCardNumber && IsCreditTransaction == false)
                {
                    IndexFirstCard = i;
                    break;
                }

            }

            if (IsConnectCredit == true)
            {
                return;
            }

            if (IsTrueUser == false && CounterСoincidence != StandartNumberOfDigitsInCardNumber)
            {
                Write("Write the name of the recipient: ");
                ReadLine();

                Write("Write the surnname of the recipient: ");
                ReadLine();

                IsUnknownUser = true;
                return;
            }

            if (CounterСoincidence == StandartNumberOfDigitsInCardNumber)
            {
                IsUnknownUser = false;

                loggedIn = true;

                if (CreditCard[IndexFirstCard].Password == Password)
                {
                    do
                    {
                        Clear();

                        Communication.GetInformationAboutUser(DebitCard[IndexFirstCard].Name, DebitCard[IndexFirstCard].Surname, StandartNumberOfDigitsInCardNumber, DebitCard[IndexFirstCard].Number);

                        CreditCard[IndexFirstCard].CheckMoney();

                        CreditCard[IndexFirstCard].ShowCredit();

                        Communication.GetCreditCardMenuInstruction();

                        Communication.GetCreditCardMenuInstruction();

                        CounterСoincidence = 0;

                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.NumPad1:

                                CreditCard[IndexFirstCard].AddMoney();

                                break;
                            case ConsoleKey.NumPad2:

                                CreditCard[IndexFirstCard].WithdrawMoney();

                                break;
                            case ConsoleKey.NumPad3:

                                if (CreditCard[IndexFirstCard].CheckCredit())
                                {
                                    IsTrueUser = false;

                                    IsErrorTransaction = false;

                                    Communication.GetMessageAboutTransfer();

                                    IsCreditTransaction = true;

                                    GetInfo();

                                    IsTrueUser = true;

                                    IsCreditTransaction = false;

                                    if (IsErrorTransaction == false)
                                    {
                                        do
                                        {
                                            Write("Enter the amount of money: ");
                                        }
                                        while (!double.TryParse(ReadLine(), out Money) || Money < 0);

                                        Write("Are you sure of your action? (please write yes-Y(y) or no-N(n)): ");

                                        do
                                        {
                                            switch (Console.ReadKey().Key)
                                            {
                                                case ConsoleKey.Enter:

                                                    if (IsUnknownUser == false)
                                                    {
                                                        CreditCard[IndexFirstCard].WithdrawTransactionMoney(Money);

                                                        if (CreditCard[IndexFirstCard].TransactionCompleted == true)
                                                        {
                                                            CreditCard[IndexSecondCard].AddTransactionMoney(Money);

                                                            Communication.GetMessageAboutSuccessfullyOperation();
                                                        }
                                                    }
                                                    else if (IsUnknownUser == true)
                                                    {
                                                        CreditCard[IndexFirstCard].WithdrawTransactionMoney(Money);

                                                        Communication.GetMessageAboutSuccessfullyOperation();

                                                        Clear();
                                                    }
                                                    break;
                                                case ConsoleKey.Escape:
                                                    Key = ConsoleKey.Escape;
                                                    break;
                                                default:
                                                    Key = UnknownKey;
                                                    break;
                                            }
                                        }
                                        while (Key == UnknownKey);

                                        IsErrorTransaction = false;
                                    }
                                    else
                                    {
                                        Clear();

                                        Communication.GetMessageAboutPayingOffCredit();
                                    }
                                }
                                break;
                            case ConsoleKey.NumPad4:

                                CreditCard[IndexFirstCard].AddCredit();

                                break;
                            case ConsoleKey.NumPad5:

                                CreditCard[IndexFirstCard].WithdrawCredit();

                                break;
                            case ConsoleKey.NumPad6:

                                IsConnectCredit = true;

                                IsTrueUser = false;

                                IsCreditTransaction = true;

                                GetInfo();

                                IsCreditTransaction = false;

                                IsTrueUser = true;

                                if (IsConnectCredit == true)
                                {
                                    IsConnectCredit = false;

                                    CreditCard[IndexFirstCard].ConnectCards(CreditCard[IndexSecondCard].Account);

                                    Communication.GetMessageAboutSuccessfullyOperation();
                                }

                                else
                                {
                                    Communication.GetMessageAboutWrongPassword();

                                }

                                IsConnectCredit = false;

                                break;
                            case ConsoleKey.NumPad7:

                                IsCancelSelection = true;

                                return;
                        }
                    }
                    while (IsCancelSelection == false);

                    IsCancelSelection = false;
                }
                else
                {
                    Communication.GetMessageAboutWrongPassword();
                }
            }

            if (loggedIn == false)
            {
                Communication.GetMessageAboutUnknownAccount();
                return;
            }

        }

        public void InputPassword()
        {
            do
            {
                Write("Write your password(4 digits): ");
                Password = Console.ReadLine();
            }
            while (!int.TryParse(Password, out int value) || Password.Length != StandartNumberOfDigitsInPassword);
        }

        public void InputNumberCard()
        {

            bool numberParse;

            do
            {
                Write("Write number of card(20 signs): ");

                string numberString = ReadLine();

                char[] numberChar = numberString.ToCharArray();

                Number = new int[numberChar.Length];

                CounterNumbers = numberChar.Length;

                numberParse = true;

                for (int i = 0; i < numberChar.Length; i++)
                {

                    if (!int.TryParse(numberChar[i].ToString(), out Number[i]))
                    {
                        numberParse = false;
                    }

                }

            }
            while (numberParse == false || CounterNumbers != StandartNumberOfDigitsInCardNumber);
        }

        public void CheckOriginality()
        {
            int numberOfTheFirstTypeOfCard = 0;

            int numberOfTheSecondTypeOfCard = 0;

            int temp;

            if (CounterDebit < CounterCredit)
            {
                CounterMaxNumberOfCard = CounterCredit;
                CounterMinNumberOfCard = CounterDebit;
            }

            else
            {
                CounterMaxNumberOfCard = CounterDebit;
                CounterMinNumberOfCard = CounterCredit;

                temp = numberOfTheFirstTypeOfCard;

                numberOfTheFirstTypeOfCard = numberOfTheSecondTypeOfCard;

                numberOfTheSecondTypeOfCard = temp;
            }

            for (numberOfTheFirstTypeOfCard = 0; numberOfTheFirstTypeOfCard < CounterMinNumberOfCard; numberOfTheFirstTypeOfCard++)
            {

                for (numberOfTheSecondTypeOfCard = 0; numberOfTheSecondTypeOfCard < CounterMaxNumberOfCard; numberOfTheSecondTypeOfCard++)
                {

                    for (int numberOfDigits = 0; numberOfDigits < StandartNumberOfDigitsInCardNumber; numberOfDigits++)
                    {

                        if (DebitCard[numberOfTheFirstTypeOfCard].Number == null || CreditCard[numberOfTheSecondTypeOfCard].Number == null)
                        {
                            break;
                        }

                        if (DebitCard[numberOfTheFirstTypeOfCard].Number[numberOfDigits] == CreditCard[numberOfTheSecondTypeOfCard].Number[numberOfDigits])
                        {
                            CounterСoincidence++;
                        }

                    }

                    if (CounterСoincidence == StandartNumberOfDigitsInCardNumber)
                    {
                        IsRegistrationSuccessfuly = false;

                        WriteLine("This account number already exists");

                        CounterСoincidence = 0;

                        return;
                    }

                    CounterСoincidence = 0;
                }
            }
        }
    }
}
