internal interface IDataBase<T, TKey>
{
    IEnumerable<T> GetAll();

    T GetById(TKey id);

    void Update(T newValue, TKey id);

    void Add(T value);
}