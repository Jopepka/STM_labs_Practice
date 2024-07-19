internal class ClientFileTable : IDataBase<Client>
{
    readonly Dictionary<int, Client> _clients;
    int newId;

    public ClientFileTable(string fileName)
    {
        _clients = CreateDict(FileLoader.Load<Client>(fileName));
        newId = _clients.Max(item => item.Key) + 1;
    }

    public void Save(string fileName) => FileLoader.Save(_clients, fileName);

    public Client GetById(int id)
    {
        CheckClientExist(id);
        return _clients[id];
    }

    public void Update(Client updatedClient, int id)
    {
        CheckClientExist(id);
        CheckUniquePassport(updatedClient, id);
        _clients[id] = updatedClient with { id = id };
    }

    public void Add(Client client)
    {
        CheckUniquePassport(client, null);
        _clients.Add(newId++, client with { id = newId });
    }

    private void CheckUniquePassport(Client client, int? ignoreClientId)
    {
        if (_clients.Any(keyValue => IsEqualsPassport(keyValue.Value, client) && keyValue.Key != ignoreClientId))
            throw new ArgumentException($"Client passport should be unique");
    }

    private void CheckClientExist(int id)
    {
        if (!_clients.ContainsKey(id))
            throw new ArgumentException($"Client with id = {id} not exist");
    }

    private bool IsEqualsPassport(Client client1, Client client2)
        => client1.PassportNumber == client2.PassportNumber && client1.PassportSeries == client2.PassportSeries;

    private Dictionary<int, Client> CreateDict(IEnumerable<Client>? items)
    {
        var itemsDict = new Dictionary<int, Client>();
        if (items is not null)
            foreach (var item in items)
                itemsDict.Add(item.id, item);
        return itemsDict;
    }
}