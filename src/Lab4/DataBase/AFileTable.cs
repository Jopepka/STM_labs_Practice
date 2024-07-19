internal abstract class AFileTable<T, K> : IDataBase<T, K>
{
    protected readonly Dictionary<K, T> _items;

    public AFileTable(string fileName) => _items = CreateDict(FileLoader.Load<T>(fileName));

    public void Save(string fileName) => FileLoader.Save(_items.Select(item => item.Value).ToArray(), fileName);

    public IEnumerable<T> GetAll() => _items.Select(item => item.Value);

    public void Add(T value)
    {
        var newId = GetNextFirstKey();
        CheckUniqueFields(value, newId);
        _items.Add(newId, SetFirstKey(value, newId));
    }

    public T GetById(K key)
    {
        CheckItemExist(key);
        return _items[key];
    }

    public void Update(T newValue, K key)
    {
        CheckItemExist(key);
        CheckUniqueFields(newValue, key);
        _items[key] = SetFirstKey(newValue, key);
    }

    private Dictionary<K, T> CreateDict(IEnumerable<T>? items)
    {
        var itemsDict = new Dictionary<K, T>();

        if (items is not null)
            foreach (var item in items)
                itemsDict.Add(GetFirstKey(item), item);

        return itemsDict;
    }

    private void CheckItemExist(K id)
    {
        if (!_items.ContainsKey(id))
            throw new ArgumentException($"Item with id = {id} not exist");
    }

    protected abstract void CheckUniqueFields(T item, K ignoreKey);

    protected abstract T SetFirstKey(T item, K firstKey);

    protected abstract K GetFirstKey(T item);

    protected abstract K GetNextFirstKey();
}
