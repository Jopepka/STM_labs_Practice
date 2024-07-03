public class Task4_Publisher
{
    event Action<string> Notify;
    readonly string _name;

    public Task4_Publisher(string name) => _name = name;

    public void Subscribe(Action<string> ev) => Notify += ev;

    public void RaiseEvent() => Notify?.Invoke(_name);
}
