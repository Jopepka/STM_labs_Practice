
internal class ClientService
{
    private readonly IDataBase<Client, int> _bd;
    private event Action<FieldChangeInfo> _notifyByUpdate;

    public ClientService(ClientFileTable bd) => _bd = bd;

    public void SubscribeOnClientUpdate(Action<FieldChangeInfo> action) => _notifyByUpdate += action;

    public IEnumerable<Client> GetAll(AccessLevel level)
    {
        var users = _bd.GetAll();
        if (level == AccessLevel.Lower)
            users = users.Select(FormatClientForLowerLevelAccess);
        return users;
    }

    public Client GetClientById(int clientId, AccessLevel level)
    {
        var user = GetClientById(clientId);
        if (level == AccessLevel.Lower)
            user = FormatClientForLowerLevelAccess(user);
        return user;
    }

    private Client GetClientById(int clientId) => _bd.GetById(clientId);

    public void AddClient(Client client, IEmployee employee)
    {
        CheckLowLevelAccess(employee.GetAccessLevel());
        _bd.Add(client);
    }

    public void UpdateFirstName(int clientId, string newName, IEmployee employee)
    {
        CheckHightLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(clientId);

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

    public void UpdateMiddleName(int clientId, string newMiddleName, IEmployee employee)
    {
        CheckHightLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(clientId);

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

    public void UpdateLastName(int clientId, string newLastName, IEmployee employee)
    {
        CheckHightLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(clientId);

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

    public void UpdatePhoneNumber(int clientId, string newPhoneNumber, IEmployee employee)
    {
        CheckLowLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(clientId);

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

    public void UpdatePassportSeries(int clientId, string newPassportSeries, IEmployee employee)
    {
        CheckHightLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(clientId);

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

    public void UpdatePassportNumber(int clientId, string newPassportNumber, IEmployee employee)
    {
        CheckHightLevelAccess(employee.GetAccessLevel());

        var client = GetClientById(clientId);

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
        _notifyByUpdate?.Invoke(change);
    }

    void CheckHightLevelAccess(AccessLevel level)
    {
        if (level < AccessLevel.High)
            throw new LowLevelAccess();
    }

    void CheckLowLevelAccess(AccessLevel level)
    {
        if (level < AccessLevel.Lower)
            throw new LowLevelAccess();
    }

    Client FormatClientForLowerLevelAccess(Client client) => client with { PassportSeries = "****", PassportNumber = "******" };
}
