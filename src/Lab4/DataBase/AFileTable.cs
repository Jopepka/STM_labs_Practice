internal abstract class AFileTable<T, TKey> : IDataBase<T, TKey>
{
    protected readonly Dictionary<TKey, T> _items;

    public AFileTable(string fileName) => _items = CreateDict(FileLoader.Load<T>(fileName));

    public void Save(string fileName) => FileLoader.Save(_items.Select(item => item.Value).ToArray(), fileName);

    public IEnumerable<T> GetAll() => _items.Select(item => item.Value);

    public void Add(T value)
    {
        var newId = GetNextFirstKey();
        CheckUniqueFields(value, newId);
        _items.Add(newId, SetFirstKey(value, newId));
    }

    public T GetById(TKey key)
    {
        CheckItemExist(key);
        return _items[key];
    }

    public void Update(T newValue, TKey key)
    {
        CheckItemExist(key);
        CheckUniqueFields(newValue, key);
        _items[key] = SetFirstKey(newValue, key);
    }

    private Dictionary<TKey, T> CreateDict(IEnumerable<T>? items)
    {
        var itemsDict = new Dictionary<TKey, T>();

        if (items is not null)
            foreach (var item in items)
                itemsDict.Add(GetFirstKey(item), item);

        return itemsDict;
    }

    private void CheckItemExist(TKey id)
    {
        if (!_items.ContainsKey(id))
            throw new ArgumentException($"Item with id = {id} not exist");
    }

    protected abstract void CheckUniqueFields(T item, TKey ignoreKey);

    protected abstract T SetFirstKey(T item, TKey firstKey);

    protected abstract TKey GetFirstKey(T item);

    protected abstract TKey GetNextFirstKey();
}
