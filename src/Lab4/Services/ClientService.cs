internal class ClientService
{
    private readonly IDataBase<Client> _bd;
    private readonly ILogger<FieldChangeInfo>? _logger;
    public ClientService(ClientFileTable bd, ILogger<FieldChangeInfo>? logger)
    {
        _bd = bd;
        _logger = logger;
    }

    public Client GetClientById(int id) => _bd.GetById(id);

    public void UpdateClient(FieldChangeInfo change, Client newClient)
    {
        _bd.Update(newClient, newClient.id);
        _logger?.Log(change);
    }

    public void AddClient(Client client)
    {

        _bd.Add(client);
    }
}
