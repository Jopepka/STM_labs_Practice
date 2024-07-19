using System.Numerics;

namespace Lab4;

class Program
{
    static ClientService userService;

    static void Main(string[] args)
    {
        string clientsLogsPath = "";
        string clientsPath = "";

        var clientsDB = new ClientFileTable(clientsPath);
        userService = new ClientService(clientsDB, new ClientsFileLogger(clientsLogsPath));

        while (true)
        {
            Console.WriteLine("Выберете роль:");
            Console.WriteLine("1. Менеджер \n2. Консультант");

            switch (Console.ReadLine())
            {
                case "1":
                    StartForMeneger();
                    break;

                case "2":
                    StartForConsultant();
                    break;

                default:
                    Console.WriteLine("Неверный номер. Попробуйте еще раз");
                    continue;
            }

            break;
        }

        Meneger employee = new Meneger(userService);

        while (true)
        {
            Console.WriteLine("Выберете действие: ");
            Console.WriteLine("1. Показать всех пользователей");
            Console.WriteLine("2. Выбрать пользователя");
            Console.WriteLine("3. Добавить пользователя");
            Console.WriteLine("4. Выход");

            switch (Console.ReadLine())
            {
                case "1":
                    break;

                case "2":
                    Console.WriteLine("Введите id пользователя: ");
                    int userId = GetNumberFromConsole<int>();
                    Console.WriteLine(ClientToString(employee.GetClient(userId)));
                    break;

                case "3":
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Неверный номер. Попробуйте еще раз");
                    continue;
            }
        }
    }

    static void StartForMeneger()
    {

    }

    static void StartForConsultant()
    {
        Consultant consultant = new Consultant(userService);

        while (true)
        {
            Console.WriteLine("Выберете действие: ");
            Console.WriteLine("1. Показать всех пользователей");
            Console.WriteLine("2. Выбрать пользователя");
            Console.WriteLine("3. Выход");

            switch (Console.ReadLine())
            {
                case "1":
                    break;
                case "2":
                    Console.WriteLine("Введите id пользователя: ");
                    int userId = GetNumberFromConsole<int>();
                    Console.WriteLine(ClientToString(consultant.GetClient(userId)));
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Неверный номер. Попробуйте еще раз");
                    continue;
            }
        }
    }

    static T GetNumberFromConsole<T>() where T : INumber<T>
    {
        T num;
        while (!T.TryParse(Console.ReadLine(), null, out num))
            Console.WriteLine("Введено не число. Попробуйте еще раз:");
        return num;
    }

    static string ClientToString(Client client)
    {
        string res = $"Name: {client.FirstName} {client.MidleName} {client.LastName}";
        res += $"\nPhone: {client.PhoneNumber}";
        res += $"\nPassport: {client.PassportNumber} {client.PassportSeries}";

        return res;
    }
}
