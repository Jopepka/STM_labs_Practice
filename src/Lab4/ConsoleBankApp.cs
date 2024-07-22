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
        StartMenu();
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

    private void StartMenu()
    {
        while (true)
        {
            try
            {
                PrintActionMenu();
                GetActionOperation(Console.ReadLine())();
            }
            catch (LowLevelAccess)
            {
                Console.WriteLine("Для операции требуется более высокий уровень доступа");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Неизвестная ошибка: \n{e}");
            }
        }
    }

    private void PrintActionMenu()
    {
        Console.WriteLine("\n\tВыберете действие: ");
        Console.WriteLine("1. Показать всех пользователей");
        Console.WriteLine("2. Редактировать пользователя");
        Console.WriteLine("3. Добавить пользователя");
        Console.WriteLine("4. Сохранить базы данных");
    }

    private Action GetActionOperation(string operationNumber) => operationNumber switch
    {
        "1" => ShowAllClients,
        "2" => StartClientRedact,
        "3" => AddNewClient,
        "4" => SaveDatabases,
        _ => () => Console.WriteLine("Неверный номер")
    };

    private void ShowAllClients()
    {
        var clients = _clientService.GetAll(_employee.AccessLevel);
        Console.WriteLine(string.Join("\n\n", clients.Select(ClientToString)));
    }

    private void AddNewClient() => _clientService.AddClient(GetClientFromConsole(), _employee);

    private void SaveDatabases()
    {
        _clientBd.Save(_clientsPath);
        _changesBd.Save(_clientsLogsPath);
    }

    private void StartClientRedact()
    {
        var client = GetClient();
        WriteClient(client);
        WriteClientChangesIfChangesExist(client.id);

        ShowChangesMenu();
        GetRedactUserOperation(Console.ReadLine())(client.id);
    }

    private Client GetClient()
    {
        Console.WriteLine("Введите id пользователя: ");
        int userId = GetNumberFromConsole<int>();
        return _clientService.GetClientById(userId, _employee.AccessLevel);
    }

    private void WriteClient(Client client)
    {
        Console.WriteLine("Выбранный клиент:");
        Console.WriteLine(ClientToString(client));
    }

    private void WriteClientChangesIfChangesExist(int clientId)
    {
        var userChanges = _changesService.FindByClientId(clientId);
        if (userChanges.Count() != 0)
        {
            Console.WriteLine("Последние изменения:");
            Console.WriteLine(ClientChangesLogToString(userChanges.MaxBy(item => item.ChangeTime)));
        }
    }

    private void ShowChangesMenu()
    {
        Console.WriteLine("\n\tИзменить:");
        Console.WriteLine("1. фамилию (доступно только менеджеру)");
        Console.WriteLine("2. имя (доступно только менеджеру)");
        Console.WriteLine("3. отчество (доступно только менеджеру)");
        Console.WriteLine("4. номер телефона");
        Console.WriteLine("5. серию паспорта (доступно только менеджеру)");
        Console.WriteLine("6. номер паспорта (доступно только менеджеру)");
    }

    private Action<int> GetRedactUserOperation(string operationNumber) => operationNumber switch
    {
        "1" => UpdateClientFirstName,
        "2" => UpdateClientMiddleName,
        "3" => UpdateClientLastName,
        "4" => UpdateClientPhoneNumber,
        "5" => UpdateClientPassportSeries,
        "6" => UpdateCLientPassportNumber,
        _ => (int plug) => Console.WriteLine("Неверный номер")
    };

    private void UpdateClientFirstName(int userId)
    {
        Console.WriteLine("Введите фамилию");
        string firstName = Console.ReadLine();
        _clientService.UpdateFirstName(userId, firstName, _employee);
    }

    private void UpdateClientMiddleName(int userId)
    {
        Console.WriteLine("Введите имя");
        string middleName = Console.ReadLine();
        _clientService.UpdateMiddleName(userId, middleName, _employee);
    }

    private void UpdateClientLastName(int userId)
    {
        Console.WriteLine("Введите отчество");
        string lastName = Console.ReadLine();
        _clientService.UpdateLastName(userId, lastName, _employee);
    }

    private void UpdateClientPhoneNumber(int userId)
    {
        Console.WriteLine("Введите номер телефона");
        string phoneNumber = Console.ReadLine();
        _clientService.UpdatePhoneNumber(userId, phoneNumber, _employee);
    }

    private void UpdateClientPassportSeries(int userId)
    {
        Console.WriteLine("Введите серию паспорта");
        string passportSeries = Console.ReadLine();
        _clientService.UpdatePassportSeries(userId, passportSeries, _employee);
    }

    private void UpdateCLientPassportNumber(int userId)
    {
        Console.WriteLine("Введите номер паспорта");
        string passportNumber = Console.ReadLine();
        _clientService.UpdatePassportNumber(userId, passportNumber, _employee);
    }

    private Client GetClientFromConsole()
    {
        Console.WriteLine("Введите фамилию:");
        var firstName = Console.ReadLine();

        Console.WriteLine("Введите имя:");
        var middleName = Console.ReadLine();

        Console.WriteLine("ВВедите отчество:");
        var LastName = Console.ReadLine();

        Console.WriteLine("Введите номер телефона:");
        var phoneNumber = Console.ReadLine();

        Console.WriteLine("Введите серию паспорта:");
        var passportSeries = Console.ReadLine();

        Console.WriteLine("Введите номер паспорта:");
        var passportNumber = Console.ReadLine();

        return new Client()
        {
            FirstName = firstName,
            MiddleName = middleName,
            LastName = LastName,
            PhoneNumber = phoneNumber,
            PassportSeries = passportSeries,
            PassportNumber = passportNumber
        };
    }

    private string ClientToString(Client client)
    {
        string res = $"Client Id: {client.id}";
        res += $"\nName: {client.FirstName} {client.MiddleName} {client.LastName}";
        res += $"\nPhone: {client.PhoneNumber}";
        res += $"\nPassport: {client.PassportNumber} {client.PassportSeries}";
        return res;
    }

    private string ClientChangesLogToString(FieldChangeInfo changeInfo)
    {

        string res = $"Record Id: {changeInfo.Id}";
        res += $"\nClient Id: {changeInfo.IdEntity}";
        res += $"\nUpdate time: {changeInfo.ChangeTime}";
        res += $"\nEditor: {changeInfo.Editor}";
        res += $"\nField name: {changeInfo.FieldName}";
        res += $"\nValue: '{changeInfo.FieldName}': {changeInfo.OldValue} -> {changeInfo.NewValue}";
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
