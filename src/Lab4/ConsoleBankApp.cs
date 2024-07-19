using System.Numerics;

internal class ConsoleBankApp
{
    string _clientsPath;
    string _clientsLogsPath;
    ClientService _clientService;
    UserChangesService _changesService;
    ClientFileTable _clientBd;
    ClientChangesFileTable _changesBd;
    IEmployee _employee;

    public ConsoleBankApp(string clientsPath, string clientsLogsPath)
    {
        _clientsPath = clientsPath;
        _clientsLogsPath = clientsLogsPath;

        _clientBd = new ClientFileTable(_clientsPath);
        _clientService = new ClientService(_clientBd);

        _changesBd = new ClientChangesFileTable(_clientsLogsPath);
        _changesService = new UserChangesService(_changesBd);

        _clientService.SubscribeOnClientUpdate(_changesService.Add);
    }

    public void Start()
    {
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
            Console.WriteLine("4. Сохранить базы данных");
            Console.WriteLine("5. Выход");

            try
            {

                switch (Console.ReadLine())
                {
                    case "1":
                        var clients = _clientService.GetAll(_employee.GetAccessLevel());
                        Console.WriteLine(string.Join("\n", clients.Select(ClientToString)));
                        break;
                    case "2":
                        StartClientRedact();
                        break;

                    case "3":
                        _clientService.AddClient(GetClientFromConsole(), _employee);
                        break;

                    case "4":
                        _clientBd.Save(_clientsPath);
                        _changesBd.Save(_clientsLogsPath);
                        break;
                    case "5":
                        return;

                    default:
                        Console.WriteLine("Неверный номер. Попробуйте еще раз");
                        continue;
                }
            }
            catch (LowLevelAccess e)
            {
                Console.WriteLine("Для операции требуется более высокий уровень доступа");
            }
        }
    }

    private void StartClientRedact()
    {
        Console.WriteLine("Введите id пользователя: ");
        int userId = GetNumberFromConsole<int>();

        var user = _clientService.GetClientById(userId, _employee.GetAccessLevel());
        var userChanges = _changesService.FindByClientId(userId);
        Console.WriteLine("Выбранный пользователь:");
        Console.WriteLine(ClientToString(user));
        if (userChanges.Count() != 0)
        {
            Console.WriteLine("Последние изменения:");
            Console.WriteLine(ClientChangesLogToString(userChanges.MaxBy(item => item.ChangeTime)));
        }

        Console.WriteLine("Изменить:");
        Console.WriteLine("1. фамилию (доступно только менеджеру)");
        Console.WriteLine("2. имя (доступно только менеджеру)");
        Console.WriteLine("3. отчество (доступно только менеджеру)");
        Console.WriteLine("4. номер телефона");
        Console.WriteLine("5. серию паспорта (доступно только менеджеру)");
        Console.WriteLine("6. номер паспорта (доступно только менеджеру)");
        Console.WriteLine("7. Назад");
        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine("Введите фамилию");
                string firstName = Console.ReadLine();
                _clientService.UpdateFirstName(userId, firstName, _employee);
                break;
            case "2":
                Console.WriteLine("Введите имя");
                string middleName = Console.ReadLine();
                _clientService.UpdateMiddleName(userId, middleName, _employee);
                break;
            case "3":
                Console.WriteLine("Введите отчество");
                string lastName = Console.ReadLine();
                _clientService.UpdateLastName(userId, lastName, _employee);
                break;
            case "4":
                Console.WriteLine("Введите номер телефона");
                string phoneNumber = Console.ReadLine();
                _clientService.UpdatePhoneNumber(userId, phoneNumber, _employee);
                break;
            case "5":
                Console.WriteLine("Введите серию паспорта");
                string passportSeries = Console.ReadLine();
                _clientService.UpdatePassportSeries(userId, passportSeries, _employee);
                break;
            case "6":
                Console.WriteLine("Введите номер паспорта");
                string passportNumber = Console.ReadLine();
                _clientService.UpdatePassportNumber(userId, passportNumber, _employee);
                break;
            case "7":
                return;
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

    private string ClientChangesLogToString(FieldChangeInfo changeInfo)
    {

        string res = "Changes";
        res += $"\nid: {changeInfo.Id}";
        res += $"\nClientId: {changeInfo.IdEntity}";
        res += $"\nUpdate time: {changeInfo.ChangeTime}";
        res += $"\nEditor: {changeInfo.Editor}";
        res += $"\nField '{changeInfo.FieldName}': {changeInfo.OldValue} -> {changeInfo.NewValue}";
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
