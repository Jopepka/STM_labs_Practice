internal class ClientChangesFileTable : AFileTable<FieldChangeInfo, int>
{
    int nextKey;

    public ClientChangesFileTable(string fileName) : base(fileName) =>
        nextKey = _items.Count() == 0 ? 0 : _items.Max(item => item.Key) + 1;

    public IEnumerable<FieldChangeInfo> FindByClientId(int clientId) =>
        _items.Where(item => item.Value.IdEntity == clientId).Select(item => item.Value);

    protected override void CheckUniqueFields(FieldChangeInfo item, int ignoreKey) { }

    protected override int GetFirstKey(FieldChangeInfo item) => item.Id;

    protected override int GetNextFirstKey() => nextKey++;

    protected override FieldChangeInfo SetFirstKey(FieldChangeInfo item, int firstKey) => item with { Id = firstKey };
}