
internal class ClientService
{
    private readonly IDataBase<Client, int> _bd;
    private readonly ILogger<FieldChangeInfo>? _logger;
    public ClientService(ClientFileTable bd, ILogger<FieldChangeInfo>? logger)
    {
        _bd = bd;
        _logger = logger;
    }

    public IEnumerable<Client> GetAll(AccessLevel level)
    {
        return _bd.GetAll();
    }

    public Client GetClientById(int id, AccessLevel level)
    {
        return _bd.GetById(id);
    }

    public void UpdateFirstName(int idClient, string newName, IEmployee employee)
    {
        CheckHightLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(idClient, employee.GetAccessLevel());

        FieldChangeInfo changeInfo = new FieldChangeInfo()
        {
            ChangeTime = DateTime.Now,
            IdEntity = client.id,
            Editor = employee.Name,
            FieldName = client.FirstName.GetType().Name,
            NewValue = newName,
            OldValue = client.FirstName,
        };

        UpdateClient(changeInfo, client with { FirstName = newName });
    }

    public void UpdateMiddleName(int idClient, string newMiddleName, IEmployee employee)
    {
        CheckHightLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(idClient, employee.GetAccessLevel());

        FieldChangeInfo changeInfo = new FieldChangeInfo()
        {
            ChangeTime = DateTime.Now,
            IdEntity = client.id,
            Editor = employee.Name,
            FieldName = client.MidleName.GetType().Name,
            NewValue = newMiddleName,
            OldValue = client.MidleName,
        };

        UpdateClient(changeInfo, client with { MidleName = newMiddleName });
    }

    public void UpdateLastName(int idClient, string newLastName, IEmployee employee)
    {
        CheckHightLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(idClient, employee.GetAccessLevel());

        FieldChangeInfo changeInfo = new FieldChangeInfo()
        {
            ChangeTime = DateTime.Now,
            IdEntity = client.id,
            Editor = employee.Name,
            FieldName = client.LastName.GetType().Name,
            NewValue = newLastName,
            OldValue = client.LastName,
        };

        UpdateClient(changeInfo, client with { MidleName = newLastName });
    }

    public void UpdatePhoneNumber(int idClient, string newPhoneNumber, IEmployee employee)
    {
        CheckLowLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(idClient, employee.GetAccessLevel());

        FieldChangeInfo changeInfo = new FieldChangeInfo()
        {
            ChangeTime = DateTime.Now,
            IdEntity = client.id,
            Editor = employee.Name,
            FieldName = client.PhoneNumber.GetType().Name,
            NewValue = newPhoneNumber,
            OldValue = client.PhoneNumber,
        };

        UpdateClient(changeInfo, client with { PhoneNumber = newPhoneNumber });
    }
    public void UpdatePassportSeries(int idClient, string newPassportSeries, IEmployee employee)
    {
        CheckHightLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(idClient, employee.GetAccessLevel());

        FieldChangeInfo changeInfo = new FieldChangeInfo()
        {
            ChangeTime = DateTime.Now,
            IdEntity = client.id,
            Editor = employee.Name,
            FieldName = client.PassportSeries.GetType().Name,
            NewValue = newPassportSeries,
            OldValue = client.PassportSeries,
        };

        UpdateClient(changeInfo, client with { PassportSeries = newPassportSeries });
    }

    public void UpdatePassportNumber(int idClient, string newPassportNumber, IEmployee employee)
    {
        CheckHightLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(idClient, employee.GetAccessLevel());

        FieldChangeInfo changeInfo = new FieldChangeInfo()
        {
            ChangeTime = DateTime.Now,
            IdEntity = client.id,
            Editor = employee.Name,
            FieldName = client.PassportNumber.GetType().Name,
            NewValue = newPassportNumber,
            OldValue = client.PassportNumber,
        };

        UpdateClient(changeInfo, client with { PassportNumber = newPassportNumber });
    }

    private void UpdateClient(FieldChangeInfo change, Client newClient)
    {
        _bd.Update(newClient, newClient.id);
        _logger?.Log(change);
    }

    public void AddClient(Client client, IEmployee employee)
    {
        CheckLowLevelAccess(employee.GetAccessLevel());
        _bd.Add(client);
    }

    void CheckHightLevelAccess(AccessLevel level)
    {
        if (level < AccessLevel.High)
            throw new Exception();
    }

    void CheckLowLevelAccess(AccessLevel level)
    {
        if (level < AccessLevel.Lower)
            throw new Exception();
    }
}
