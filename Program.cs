using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using NLog;
using NLog.Config;
using NLog.Targets;

internal class Program
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    public static void Main()
    {
        SupportBank.GetJSONTransactions();
        // var config = new LoggingConfiguration();
        // var target = new FileTarget { FileName = @"C:\Users\AliBar\Documents\Training\SupportBank\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
        // config.AddTarget("File Logger", target);
        // config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
        // LogManager.Configuration = config;

        // Logger.Info("Program started running.");



        // var transactions = SupportBank.GetCSVTransactions();
        // Logger.Info("Sucessfully got list of valid transactions");

        // var uniqueNames = SupportBank.GetUniqueNames(transactions);
        // var accounts = SupportBank.CreateAccounts(uniqueNames);
        // Logger.Info("Sucessfully created accounts");


        // int initialInput;
        // Logger.Info("Starting user input");
        // Console.WriteLine("Do you want to list all accounts or a specific account? Press 1 to run all accounts or 2 to run a specific account.");
        // bool isInitialInputValid = int.TryParse(Console.ReadLine(), out initialInput);
        // while (!isInitialInputValid || initialInput != 1 && initialInput != 2)
        // {
        //     Console.WriteLine("Please press 1 or 2 to run the program.");
        //     isInitialInputValid = int.TryParse(Console.ReadLine(), out initialInput);
        // }

        // if (initialInput == 1)
        // {
        //     SupportBank.ListAll(accounts, transactions);
        //     Console.WriteLine("\nDo you need another service? Press 0 to return to the beginning or any other key to exit.");
        //     int userChoice;
        //     bool isUserChoiceValid = int.TryParse(Console.ReadLine(), out userChoice);
        //     if (isUserChoiceValid && userChoice == 0)
        //     {
        //         Logger.Info("Restarting program . . .");
        //         Program.Main();
        //     }
        // }

        // if (initialInput == 2)
        // {
        //     Console.WriteLine("To view transactions, please enter a name.");
        //     var userNameInput = Console.ReadLine();
        //     while (!uniqueNames.Contains(userNameInput))
        //     {
        //         Console.WriteLine("Please enter a valid name.");
        //         userNameInput = Console.ReadLine();
        //     }
        //     SupportBank.ListAccount(userNameInput, transactions);
        //     Console.WriteLine("\nDo you need another service? Press 0 to return to the beginning or any other key to exit.");
        //     int userChoice;
        //     bool isUserChoiceValid = int.TryParse(Console.ReadLine(), out userChoice);
        //     if (isUserChoiceValid && userChoice == 0)
        //     {
        //         Logger.Info("Restarting program . . .");
        //         Program.Main();
        //     }
        // }
    }
}


