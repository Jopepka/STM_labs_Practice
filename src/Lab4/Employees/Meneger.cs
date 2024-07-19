internal class Meneger : Consultant
{
    public Meneger(ClientService userService) : base(userService) { }

    public void UpdateFirstName(Client client, string firstName)
    {
        var changes = new FieldChangeInfo()
        {
            FieldName = nameof(client.FirstName),
            OldValue = client.FirstName,
            NewValue = firstName,
        };

        UpdateClient(changes, client with { FirstName = firstName });
    }

    public void UpdateMiddleName(Client client, string middleName)
    {
        var changes = new FieldChangeInfo()
        {
            FieldName = nameof(middleName),
            OldValue = client.MidleName,
            NewValue = middleName,
        };

        UpdateClient(changes, client with { MidleName = middleName });
    }

    public void UpdateLastName(Client client, string lastName)
    {
        var changes = new FieldChangeInfo()
        {
            FieldName = nameof(client.LastName),
            OldValue = client.LastName,
            NewValue = lastName,
        };


        UpdateClient(changes, client with { LastName = lastName });
    }

    public void UpdatePassportNumber(Client client, int passportNumber)
    {
        var changes = new FieldChangeInfo()
        {
            FieldName = nameof(client.PassportNumber),
            OldValue = client.PassportNumber.ToString(),
            NewValue = passportNumber.ToString(),
        };


        UpdateClient(changes, client with { PassportNumber = passportNumber });
    }

    public void UpdatePassportSeries(Client client, int passportSeries)
    {
        var changes = new FieldChangeInfo()
        {
            FieldName = nameof(client.PassportSeries),
            OldValue = client.PassportSeries.ToString(),
            NewValue = passportSeries.ToString(),
        };


        UpdateClient(changes, client with { PassportSeries = passportSeries });
    }

    public void AddClient(Client client)
    {
        _userService.
    }

    public override Client GetClient(int id) => _userService.GetClientById(id);
}
