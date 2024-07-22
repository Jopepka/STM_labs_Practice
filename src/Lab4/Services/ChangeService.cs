
internal class UserChangesService
{
    private readonly ClientChangesFileTable _bd;

    public UserChangesService(ClientChangesFileTable bd) => _bd = bd;

    public void Add(FieldChangeInfo changes) => _bd.Add(changes);

    public IEnumerable<FieldChangeInfo> FindByClientId(int clientId) => _bd.FindByClientId(clientId);
}