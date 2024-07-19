using System.Numerics;

internal class ConsoleBankApp
{
    string _clientsPath;
    string _clientsLogsPath;
    ClientService _userService;
    AFileTable<Client, int> _bd;
    IEmployee _employee;

    public ConsoleBankApp(string clientsPath, string clientsLogsPath)
    {
        _clientsPath = clientsPath;
        _clientsLogsPath = clientsLogsPath;
    }

    public void Start()
    {
        var bd = new ClientFileTable(_clientsPath);
        _userService = new ClientService(bd, new ClientsFileLogger(_clientsLogsPath));
        _bd = bd;
        SelectRole();
        SelectOperation();
    }

    private void SelectRole()
    {
        while (true)
        {
            Console.WriteLine("Выберете роль:");
            Console.WriteLine("1. Менеджер \n2. Консультант");

            switch (Console.ReadLine())
            {
                case "1":
                    _employee = new Meneger("Менеджер");
                    break;

                case "2":
                    _employee = new Consultant("Консультант");
                    break;

                default:
                    Console.WriteLine("Неверный номер. Попробуйте еще раз");
                    continue;
            }

            break;
        }
    }

    private void SelectOperation()
    {
        while (true)
        {
            Console.WriteLine("Выберете действие: ");
            Console.WriteLine("1. Показать всех пользователей");
            Console.WriteLine("2. Выбрать пользователя");
            Console.WriteLine("3. Добавить пользователя");
            Console.WriteLine("4. Сохранить базу данных");
            Console.WriteLine("5. Выход");

            switch (Console.ReadLine())
            {
                case "1":
                    var clients = _userService.GetAll(_employee.GetAccessLevel());
                    Console.WriteLine(string.Join("\n", clients.Select(ClientToString)));
                    break;
                case "2":
                    StartClientRedact();
                    break;

                case "3":
                    _userService.AddClient(GetClientFromConsole(), _employee);
                    break;

                case "4":
                    _bd.Save(_clientsPath);
                    break;
                case "5":
                    return;

                default:
                    Console.WriteLine("Неверный номер. Попробуйте еще раз");
                    continue;
            }
        }
    }

    private void StartClientRedact()
    {
        Console.WriteLine("Введите id пользователя: ");
        int userId = GetNumberFromConsole<int>();

        Console.WriteLine("Изменить:");
        Console.WriteLine("1. фамилию (доступно только менеджеру)");
        Console.WriteLine("2. имя (доступно только менеджеру)");
        Console.WriteLine("3. отчество (доступно только менеджеру)");
        Console.WriteLine("4. номер телефона");
        Console.WriteLine("5. серию паспорта (доступно только менеджеру)");
        Console.WriteLine("6. номер паспорта (доступно только менеджеру)");
        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine("Введите фамилию");
                string firstName = Console.ReadLine();
                _userService.UpdateFirstName(userId, firstName, _employee);
                break;
            case "2":
                Console.WriteLine("Введите имя");
                string middleName = Console.ReadLine();
                _userService.UpdateMiddleName(userId, middleName, _employee);
                break;
            case "3":
                Console.WriteLine("Введите отчество");
                string lastName = Console.ReadLine();
                _userService.UpdateLastName(userId, lastName, _employee);
                break;
            case "4":
                Console.WriteLine("Введите номер телефона");
                string phoneNumber = Console.ReadLine();
                _userService.UpdatePhoneNumber(userId, phoneNumber, _employee);
                break;
            case "5":
                Console.WriteLine("Введите серию паспорта");
                string passportSeries = Console.ReadLine();
                _userService.UpdatePassportSeries(userId, passportSeries, _employee);
                break;
            case "6":
                Console.WriteLine("Введите номер паспорта");
                string passportNumber = Console.ReadLine();
                _userService.UpdatePassportNumber(userId, passportNumber, _employee);
                break;
            default:
                return;
        }
        Console.WriteLine("Данные обновлены");
    }

    private Client GetClientFromConsole()
    {
        Console.WriteLine("Введите фамилию");
        var firstName = Console.ReadLine();

        Console.WriteLine("Введите имя");
        var middleName = Console.ReadLine();

        Console.WriteLine("ВВедите отчество");
        var LastName = Console.ReadLine();

        Console.WriteLine("Введите номер телефона");
        var phoneNumber = Console.ReadLine();

        Console.WriteLine("Введите серию паспорта");
        var passportSeries = Console.ReadLine();

        Console.WriteLine("Введите номер паспорта");
        var passportNumber = Console.ReadLine();

        return new Client()
        {
            FirstName = firstName,
            MidleName = middleName,
            LastName = LastName,
            PhoneNumber = phoneNumber,
            PassportSeries = passportSeries,
            PassportNumber = passportNumber
        };
    }

    private string ClientToString(Client client)
    {
        string res = $"id: {client.id}";
        res += $"\nName: {client.FirstName} {client.MidleName} {client.LastName}";
        res += $"\nPhone: {client.PhoneNumber}";
        res += $"\nPassport: {client.PassportNumber} {client.PassportSeries}";
        return res;
    }

    static T GetNumberFromConsole<T>() where T : INumber<T>
    {
        T num;
        while (!T.TryParse(Console.ReadLine(), null, out num))
            Console.WriteLine("Введено не число. Попробуйте еще раз:");
        return num;
    }
}
