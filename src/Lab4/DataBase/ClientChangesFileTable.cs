internal class ClientChangesFileTable : IDataBase<FieldChangeInfo>
{
    readonly Dictionary<int, FieldChangeInfo> _fieldChanges;
    int newId;

    public ClientChangesFileTable(string fileName)
    {
        _fieldChanges = CreateDict(FileLoader.Load<FieldChangeInfo>(fileName));
        newId = _fieldChanges.Max(item => item.Key) + 1;
    }

    public void Save(string fileName) => FileLoader.Save(_fieldChanges, fileName);

    public FieldChangeInfo GetById(int id)
    {
        CheckItemExist(id);
        return _fieldChanges[id];
    }

    public void Update(FieldChangeInfo newChangeInfo, int id)
    {
        CheckItemExist(id);
        _fieldChanges[id] = newChangeInfo with { Id = id };
    }

    public void Add(FieldChangeInfo changeInfo)
    {
        _fieldChanges.Add(newId++, changeInfo with { Id = newId });
    }

    private void CheckItemExist(int id)
    {
        if (!_fieldChanges.ContainsKey(id))
            throw new ArgumentException($"Item with id = {id} not exist");
    }

    private Dictionary<int, FieldChangeInfo> CreateDict(IEnumerable<FieldChangeInfo>? items)
    {
        var itemsDict = new Dictionary<int, FieldChangeInfo>();

        if (items is not null)
            foreach (var item in items)
                itemsDict.Add(item.Id, item);

        return itemsDict;
    }
}