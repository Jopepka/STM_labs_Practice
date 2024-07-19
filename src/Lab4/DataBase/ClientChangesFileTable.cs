internal class ClientChangesFileTable : AFileTable<FieldChangeInfo, int>
{
    int nextKey;

    public ClientChangesFileTable(string fileName) : base(fileName) => nextKey = _items.Max(item => item.Key) + 1;

    protected override void CheckUniqueFields(FieldChangeInfo item, int ignoreKey) { }

    protected override int GetFirstKey(FieldChangeInfo item) => item.Id;

    protected override int GetNextFirstKey() => nextKey++;

    protected override FieldChangeInfo SetFirstKey(FieldChangeInfo item, int firstKey) => item with { Id = firstKey };
}