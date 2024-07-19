internal class Consultant : IEmployee
{
    protected readonly ClientService _userService;

    public Consultant(ClientService userService) => _userService = userService;

    public void UpdatePhoneNumber(Client client, int phoneNumber)
    {
        var changes = new FieldChangeInfo()
        {
            FieldName = "PhoneNumber",
            OldValue = client.PhoneNumber.ToString(),
            NewValue = phoneNumber.ToString()
        };

        var updatedClient = client with { PhoneNumber = phoneNumber };
        UpdateClient(changes, updatedClient);
    }

    protected void UpdateClient(FieldChangeInfo changes, Client client)
    {
        changes = changes with { ChangeTime = DateTime.Now, Editor = GetType().Name };
        _userService.UpdateClient(changes, client);
    }

    public virtual Client GetClient(int id) => _userService.GetClientById(id) with { PassportNumber = 0, PassportSeries = 0 };
}