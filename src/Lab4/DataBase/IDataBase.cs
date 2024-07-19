internal interface IDataBase<T, K>
{
    T GetById(K id);

    void Update(T newValue, K id);

    void Add(T value);
}