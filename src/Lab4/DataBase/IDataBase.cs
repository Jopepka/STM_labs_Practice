internal interface IDataBase<T>
{
    T GetById(int id);

    void Update(T newValue, int id);

    void Add(T value);
}