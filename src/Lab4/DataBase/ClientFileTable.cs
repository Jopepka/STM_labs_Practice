internal class ClientFileTable : AFileTable<Client, int>
{
    int newId;

    public ClientFileTable(string fileName) : base(fileName) => newId = _items.Max(item => item.Key) + 1;

    protected override void CheckUniqueFields(Client item, int ignoreKey)
    {
        if (_items.Any(keyValue => IsEqualsPassport(keyValue.Value, item) && keyValue.Key != ignoreKey))
            throw new ArgumentException($"Client passport should be unique");
    }

    private bool IsEqualsPassport(Client client1, Client client2)
        => client1.PassportNumber == client2.PassportNumber && client1.PassportSeries == client2.PassportSeries;

    protected override Client SetFirstKey(Client item, int firstKey) => item with { id = firstKey };

    protected override int GetFirstKey(Client item) => item.id;

    protected override int GetNextFirstKey() => newId++;
}