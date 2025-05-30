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
        var config = new LoggingConfiguration();
        var target = new FileTarget { FileName = @".\SupportBank\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
        config.AddTarget("File Logger", target);
        config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
        LogManager.Configuration = config;

        Logger.Info("Program started running.");

        string filePath;
        List<Transaction> transactions = new List<Transaction>();

        Console.WriteLine("Which year's data do you want to view? 2013, 2014 or 2015?");
        var firstUserInput = Console.ReadLine();
        if (firstUserInput == "2013") {
            filePath = "./Transactions2013.json";
            var jsonTransactions = SupportBank.GetJSONTransactions(filePath);
            transactions.AddRange(SupportBank.JsonConversion(jsonTransactions));
        } else if (firstUserInput == "2014") {
            filePath = "./Transactions2014.csv";
             transactions.AddRange(SupportBank.GetCSVTransactions(filePath));
        } else if (firstUserInput == "2015") {
            filePath = "./DodgyTransactions2015.csv";
            transactions.AddRange(SupportBank.GetCSVTransactions(filePath));
        } else {
            while (firstUserInput != "2013" && firstUserInput != "2014" && firstUserInput != "2015") {
                Console.WriteLine("Please enter a valid year.");
                firstUserInput = Console.ReadLine();
            }
        }
    
        Logger.Info("Sucessfully got list of valid transactions");
        var uniqueNames = SupportBank.GetUniqueNames(transactions);
        var accounts = SupportBank.CreateAccounts(uniqueNames);
        Logger.Info("Sucessfully created accounts");

        int initialInput;
        Logger.Info("Starting user input");
        Console.WriteLine("Do you want to list all accounts or a specific account? Press 1 to run all accounts or 2 to run a specific account.");
        bool isInitialInputValid = int.TryParse(Console.ReadLine(), out initialInput);
        while (!isInitialInputValid || initialInput != 1 && initialInput != 2)
        {
            Console.WriteLine("Please press 1 or 2 to run the program.");
            isInitialInputValid = int.TryParse(Console.ReadLine(), out initialInput);
        }

        if (initialInput == 1)
        {
            SupportBank.ListAll(accounts, transactions);
            Console.WriteLine("\nDo you need another service? Press 0 to return to the beginning or any other key to exit.");
            int userChoice;
            bool isUserChoiceValid = int.TryParse(Console.ReadLine(), out userChoice);
            if (isUserChoiceValid && userChoice == 0)
            {
                Logger.Info("Restarting program . . .");
                Program.Main();
            }
        }

        if (initialInput == 2)
        {
            Console.WriteLine("To view transactions, please enter a name.");
            var userNameInput = Console.ReadLine();
            while (!uniqueNames.Contains(userNameInput))
            {
                Console.WriteLine("Please enter a valid name.");
                userNameInput = Console.ReadLine();
            }
            SupportBank.ListAccount(userNameInput, transactions);
            Console.WriteLine("\nDo you need another service? Press 0 to return to the beginning or any other key to exit.");
            int userChoice;
            bool isUserChoiceValid = int.TryParse(Console.ReadLine(), out userChoice);
            if (isUserChoiceValid && userChoice == 0)
            {
                Logger.Info("Restarting program . . .");
                Program.Main();
            }
        }
    }
}


