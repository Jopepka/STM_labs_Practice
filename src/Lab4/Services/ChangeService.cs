
internal class ChangesService
{
    private readonly ClientChangesFileTable _bd;
    public ChangesService(ClientChangesFileTable bd) => _bd = bd;

    public void Add(FieldChangeInfo changes) => _bd.Add(changes);
}