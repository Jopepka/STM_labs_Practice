namespace Lab4;

class Program
{
    static void Main(string[] args)
    {
        string clientsDataPath = "ClientsDatas.json";
        string clientsLogsPath = "ClientsLogs.json";

        var consoleBank = new ConsoleBankApp(clientsDataPath, clientsLogsPath);

        consoleBank.Start();
    }
}
