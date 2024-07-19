internal interface IDataBase<T, K>
{
    IEnumerable<T> GetAll();

    T GetById(K id);

    void Update(T newValue, K id);

    void Add(T value);
}